using KybInfrastructure.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartLockDemo.Data.Utilities;
using SmartLockDemo.Infrastructure.Extensions;
using StackExchange.Redis;
using System.Collections.Generic;

namespace SmartLockDemo.Data
{
    /// <summary>
    /// Describes all dependencies of SmartLockDemo.Data module
    /// </summary>
    public class ModuleDescriptor : ModuleDescriptorBase<ModuleContext>
    {
        internal static ModuleContext moduleContext = new(string.Empty, string.Empty, string.Empty, false);

        public ModuleDescriptor(ModuleContext context, string adminEmail, string adminHashedPassword) : base(new List<ServiceDescriptor>
        {
            new ServiceDescriptor(typeof(IUnitOfWork), (serviceProvider) => new UnitOfWork(new SmartLockDemoDbContext(),
                serviceProvider.GetRequiredService<IRedisClient>()),
                    ServiceLifetime.Scoped)
        }.AddIfConditionSatisfied(() => context.IsRedisActive, new ServiceDescriptor(typeof(IRedisClient), (serviceProvider) =>
        {
            try
            {
                ConfigurationOptions options = new();
                options.EndPoints.Add($"{context.RedisUri}:{context.RedisPort}");
                options.AbortOnConnectFail = false;
                options.ConnectTimeout = 10;

                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(options);

                return new RedisClient(redis.GetDatabase());
            }
            catch (System.Exception ex)
            {
                serviceProvider.GetRequiredService<ILogger>()?.LogError("RedisClient couldn't be constructed: ", ex);
                moduleContext.IsRedisActive = false;

                return default;
            }
        }, ServiceLifetime.Singleton)),
            context)
        {
            moduleContext = context;

            using SmartLockDemoDbContext dbContext = new();
            if (dbContext.Database.EnsureCreated())
            {
                dbContext.Users.Add(new Entities.User
                {
                    Email = adminEmail,
                    HashedPassword = adminHashedPassword
                });
                dbContext.SaveChanges();
            }
        }
    }
}
