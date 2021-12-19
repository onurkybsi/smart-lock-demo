using KybInfrastructure.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace SmartLockDemo.Data
{
    /// <summary>
    /// Describes all dependencies of SmartLockDemo.Data module
    /// </summary>
    public class ModuleDescriptor : ModuleDescriptorBase<ModuleContext>
    {
        internal static ModuleContext moduleContext;
        public ModuleDescriptor(ModuleContext context, string adminEmail, string adminHashedPassword) : base(new List<ServiceDescriptor>
        {
            new ServiceDescriptor(typeof(IUnitOfWork), (serviceProvider) => new UnitOfWork(new SmartLockDemoDbContext()),
                    ServiceLifetime.Scoped)
        }, context)
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
