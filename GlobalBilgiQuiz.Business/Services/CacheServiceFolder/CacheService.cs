using System.Text.Json;
using System.Text.Json.Serialization;
using GlobalBilgiQuiz.Data.Services.RedisServiceFolder;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;


namespace GlobalBilgiQuiz.Business.Services.CacheServiceFolder
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase? _cache;
        JsonSerializerOptions options;
        public CacheService(IConfiguration configuration)
        {
            try
            {
                var connection = ConnectionMultiplexer.Connect(configuration.GetSection("ConnectionStrings:RedisConnection")?.Value);
                _cache = connection.GetDatabase();

                options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Converters = { new JsonStringEnumConverter() },
                    IgnoreNullValues = true
                };
            }
            catch (Exception)
            {
                _cache = null;

            }
        }

        public T? Get<T>(string key)
        {
            string? cachedValue = _cache.StringGet(key);
            if (!string.IsNullOrEmpty(cachedValue))
            {
                return JsonSerializer.Deserialize<T>(cachedValue);
            }

            return default;
        }
        public bool Set<T>(string key, T value, DateTimeOffset expirationTime)
        {
            if (_cache != null)
            {
                var serializedValue = JsonSerializer.Serialize(value, options);
                return _cache.StringSet(key, serializedValue);
            }
            return false;
        }
        public bool Set<T>(string key, IEnumerable<T> value, DateTimeOffset expirationTime)
        {
            if (_cache != null)
            {
                return Set(key, value, expirationTime);
            }
            return false;

        }

        public bool Remove(string key)
        {
            if (_cache != null)
            {
                var keyExists = _cache.KeyExists(key);
                return keyExists && _cache.KeyDelete(key);
            }
            return default;

        }

        public bool Exists(string key)
        {
            if (_cache != null)
            {
                return _cache.KeyExists(key);
            }
            return false;
        }
    }
}
