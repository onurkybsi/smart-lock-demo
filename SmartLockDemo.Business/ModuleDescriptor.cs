using KybInfrastructure.Core;
using Microsoft.Extensions.DependencyInjection;
using SmartLockDemo.Business.Service.User;
using System.Collections.Generic;

namespace SmartLockDemo.Business
{
    /// <summary>
    /// Describes all dependencies of SmartLockDemo.Business module
    /// </summary>
    public class ModuleDescriptor : ModuleDescriptorBase<ModuleContext>
    {
        public ModuleDescriptor() : base(new List<ServiceDescriptor>
        {
            ServiceDescriptor.Describe(typeof(IValidatorAccessor), typeof(ValidatorAccessor), ServiceLifetime.Singleton),
            ServiceDescriptor.Describe(typeof(IUserService), typeof(UserService), ServiceLifetime.Scoped 
                /*TO-DO: ServiceAccessor should develop, and this lifetime should change to singleton*/)
        })
        {
            FluentValidation.ValidatorOptions.Global.LanguageManager.Enabled = false;
        }
    }
}
