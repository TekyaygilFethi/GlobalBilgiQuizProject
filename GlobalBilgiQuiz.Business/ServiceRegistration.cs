using GlobalBilgiQuiz.Business.Services.RedisServiceFolder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GlobalBilgiQuiz.Business
{
    public static class ServiceRegistration
    {

        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
            services.AddSingleton<RedisService>(sp =>
            {
                var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
                var redis = new RedisService(redisSettings.Host, redisSettings.Port);
                redis.Connect();
                return redis;
            });
        }
    }
}
