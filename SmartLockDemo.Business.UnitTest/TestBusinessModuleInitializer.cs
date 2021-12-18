using Microsoft.Extensions.DependencyInjection;
using Moq;
using SmartLockDemo.Data;
using SmartLockDemo.Data.Repositories;
using SmartLockDemo.Infrastructure.Utilities;
using System;
using System.Collections.Generic;

namespace SmartLockDemo.Business.UnitTest
{
    internal class TestBusinessModuleInitializer
    {
        private IServiceProvider serviceProvider;

        public TestBusinessModuleInitializer()
        {
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository).Returns((new Mock<IUserRepository>()).Object);
            mockUnitOfWork.Setup(muw => muw.DoorRepository).Returns((new Mock<IDoorRepository>()).Object);
            mockUnitOfWork.Setup(muw => muw.TagRepository).Returns((new Mock<ITagRepository>()).Object);
            mockUnitOfWork.Setup(muw => muw.TagDoorRepository).Returns((new Mock<ITagDoorRepository>()).Object);
            mockUnitOfWork.Setup(muw => muw.UserTagRepository).Returns((new Mock<IUserTagRepository>()).Object);
            Mock<IEncryptionUtilities> mockEncryptionUtilities = new();

            BuildServiceProvider(mockUnitOfWork.Object, mockEncryptionUtilities.Object);
        }

        private void BuildServiceProvider(IUnitOfWork mockUnitOfWorkObject, IEncryptionUtilities mockEncryptionUtilitiesObject)
        {
            List<ServiceDescriptor> dataModuleDescriptions = new()
            {
                ServiceDescriptor.Describe(typeof(IUnitOfWork), (serviceProvider) => mockUnitOfWorkObject, ServiceLifetime.Transient),
                ServiceDescriptor.Describe(typeof(IEncryptionUtilities), (serviceProvider) => mockEncryptionUtilitiesObject, ServiceLifetime.Transient)
            };
            IServiceCollection businessServicesCollection = new ServiceCollection();
            dataModuleDescriptions.ForEach(dataServiceDescriptor => businessServicesCollection.Add(dataServiceDescriptor));

            (new Business.ModuleDescriptor()).Describe(businessServicesCollection);
            serviceProvider = businessServicesCollection.BuildServiceProvider();
        }

        public TestBusinessModuleInitializer(IUnitOfWork mockUnitOfWorkObject, IEncryptionUtilities mockEncryptionUtilitiesObject)
            => BuildServiceProvider(mockUnitOfWorkObject, mockEncryptionUtilitiesObject);

        public TService GetService<TService>()
            => (TService)serviceProvider.GetService(typeof(TService));
    }
}
