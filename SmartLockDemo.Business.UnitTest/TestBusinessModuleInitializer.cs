﻿using Microsoft.Extensions.DependencyInjection;
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
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(muw => muw.UserRepository).Returns((new Mock<IUserRepository>()).Object);
            Mock<IEncryptionUtilities> mockEncryptionUtilities = new Mock<IEncryptionUtilities>();

            BuildServiceProvider(mockUnitOfWork.Object, mockEncryptionUtilities.Object);
        }

        private void BuildServiceProvider(IUnitOfWork mockUnitOfWorkObject, IEncryptionUtilities mockEncryptionUtilitiesObject)
        {
            List<ServiceDescriptor> dataModuleDescriptions = new List<ServiceDescriptor> {
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