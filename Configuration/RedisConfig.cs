using Fundamentos.Redis.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Redis;

namespace Fundamentos.Redis.Configuration
{
    public static class RedisConfig
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var redisSection = configuration.GetSection("RedisSettings");
            var redisSettings = redisSection.Get<RedisSettings>();
            services.Configure<RedisSettings>(redisSection);

            services.AddSingleton<IRedisClientsManager>((sp) => new RedisManagerPool(configuration["RedisSettings:RedisConnection"]));
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = redisSettings.RedisConnection;
                options.InstanceName = redisSettings.InstanceName;
            });

            return services;
        }
    }
}
