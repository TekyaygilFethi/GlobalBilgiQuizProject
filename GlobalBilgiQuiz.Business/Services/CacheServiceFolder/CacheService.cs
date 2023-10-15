using System.Text.Json;
using System.Text.Json.Serialization;
using GlobalBilgiQuiz.Data.Services.RedisServiceFolder;
using StackExchange.Redis;


namespace GlobalBilgiQuiz.Business.Services.CacheServiceFolder
{
    public class CacheService<T> : ICacheService<T>
    {
        private readonly IDatabase? _cache;

        public CacheService(RedisConfiguration config)
        {
            try
            {
                var connection = ConnectionMultiplexer.Connect(config.RedisConnection);
                _cache = connection.GetDatabase();
            }
            catch (Exception)
            {
                _cache = null;

            }
        }

        public T? Get<T>(string key)
        {
            if (_cache != null)
            {
                string? cachedValue = _cache.StringGet(key);
                if (!string.IsNullOrEmpty(cachedValue))
                {
                    return JsonSerializer.Deserialize<T>(cachedValue);
                }
                return default;
            }
            return default;
        }
        public bool Set<T>(string key, T value, DateTimeOffset expirationTime)
        {
            if (_cache != null)
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Converters = { new JsonStringEnumConverter() },
                    IgnoreNullValues = true
                };

                var serializedValue = JsonSerializer.Serialize(value, options);
                return _cache.StringSet(key, serializedValue);
            }
            return false;
        }
        public bool Set<T>(string key, IEnumerable<T> value, DateTimeOffset expirationTime)
        {
            if (_cache != null)
            {
                return Set(key, value.ToList(), expirationTime);
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
