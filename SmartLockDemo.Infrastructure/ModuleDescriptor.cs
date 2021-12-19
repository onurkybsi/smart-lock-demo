using KybInfrastructure.Core;
using Microsoft.Extensions.DependencyInjection;
using SmartLockDemo.Infrastructure.Utilities;
using System.Collections.Generic;

namespace SmartLockDemo.Infrastructure
{
    /// <summary>
    /// Describes all dependencies of SmartLockDemo.Infrastructure module
    /// </summary>
    public class ModuleDescriptor : ModuleDescriptorBase<ModuleContext>
    {
        public ModuleDescriptor(ModuleContext context) : base(new List<ServiceDescriptor>
        {
            ServiceDescriptor.Describe(typeof(IEncryptionUtilities), (serviceProvider) => new EncryptionUtilities(context.HashingSalt,
                context.BearerTokenSecurityKey, context.ValidityPeriodOfBearerTokenInMs),
                ServiceLifetime.Singleton)
        })
        { }
    }
}
