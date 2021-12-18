using KybInfrastructure.Core;
using Microsoft.Extensions.DependencyInjection;
using SmartLockDemo.Business.Service.SmartLockAdministration;
using SmartLockDemo.Business.Service.User;
using SmartLockDemo.Business.Utilities;
using SmartLockDemo.Data;
using SmartLockDemo.Infrastructure.Utilities;
using System;
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
            ServiceDescriptor.Describe(typeof(IValidatorAccessor), typeof(ValidatorAccessor), ServiceLifetime.Scoped),
            ServiceDescriptor.Describe(typeof(IUserService), typeof(UserService), ServiceLifetime.Scoped 
                /*TO-DO: ServiceAccessor should develop, and this lifetime should change to singleton*/),
            ServiceDescriptor.Describe(typeof(IAdministrationService), typeof(AdministrationService),
                ServiceLifetime.Scoped),
        }, new List<Type>
        {
            /* Dependencies of Business module */
            typeof(IUnitOfWork), typeof(IEncryptionUtilities)
        })
        {
            FluentValidation.ValidatorOptions.Global.LanguageManager.Enabled = false;
        }
    }
}
