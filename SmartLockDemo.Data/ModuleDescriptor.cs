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
        public ModuleDescriptor(ModuleContext context) : base(new List<ServiceDescriptor>
        {
            new ServiceDescriptor(typeof(IUnitOfWork), (serviceProvider) => new UnitOfWork(new SmartLockDemoDbContext()),
                    ServiceLifetime.Scoped)
        }, context)
        {
            moduleContext = context;
        }
    }
}
