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
        public ModuleDescriptor() : base(new List<ServiceDescriptor>
        {
            new ServiceDescriptor(typeof(IUnitOfWork), (serviceProvider) => new UnitOfWork(new SmartLockDemoDbContext()),
                    ServiceLifetime.Scoped)
        })
        { }
    }
}
