using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace SmartLockDemo.Business.UnitTest
{
    internal class BusinessModuleInitializer
    {
        private static BusinessModuleInitializer instance;

        public static BusinessModuleInitializer Init(List<ServiceDescriptor> dataServiceDescriptions) => instance ??= new(dataServiceDescriptions);


        private readonly IServiceProvider serviceProvider;

        private BusinessModuleInitializer(List<ServiceDescriptor> dataServiceDescriptions)
        {
            IServiceCollection businessServicesCollection = new ServiceCollection();
            dataServiceDescriptions.ForEach(dataServiceDescriptor => businessServicesCollection.Add(dataServiceDescriptor));

            (new Business.ModuleDescriptor()).Describe(businessServicesCollection);
            serviceProvider = businessServicesCollection.BuildServiceProvider();
        }

        public TService GetService<TService>()
            => (TService)serviceProvider.GetService(typeof(TService));
    }
}
