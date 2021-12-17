using Microsoft.Extensions.DependencyInjection;
using Moq;
using SmartLockDemo.Data;
using SmartLockDemo.Infrastructure.Utilities;
using System;
using System.Collections.Generic;

namespace SmartLockDemo.Business.UnitTest
{
    internal class TestBusinessModuleInitializer
    {
        private Mock<IUnitOfWork> mockUnitOfWork;
        private Mock<IEncryptionUtilities> mockEncryptionUtilities;

        private readonly IServiceProvider serviceProvider;

        public TestBusinessModuleInitializer()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockEncryptionUtilities = new Mock<IEncryptionUtilities>();

            List<ServiceDescriptor> dataModuleDescriptions = new List<ServiceDescriptor> {
                    ServiceDescriptor.Describe(typeof(IUnitOfWork), (serviceProvider) => mockUnitOfWork.Object, ServiceLifetime.Transient),
                    ServiceDescriptor.Describe(typeof(IEncryptionUtilities), (serviceProvider) => mockEncryptionUtilities.Object, ServiceLifetime.Transient)
            };
            IServiceCollection businessServicesCollection = new ServiceCollection();
            dataModuleDescriptions.ForEach(dataServiceDescriptor => businessServicesCollection.Add(dataServiceDescriptor));

            (new Business.ModuleDescriptor()).Describe(businessServicesCollection);
            serviceProvider = businessServicesCollection.BuildServiceProvider();
        }

        public Mock<IUnitOfWork> GetMockUnitOfWork()
            => mockUnitOfWork;

        public Mock<IEncryptionUtilities> GetMockEncryptionUtilities()
            => mockEncryptionUtilities;

        public TService GetService<TService>()
            => (TService)serviceProvider.GetService(typeof(TService));
    }
}
