using Microsoft.Extensions.DependencyInjection;
using Moq;
using SmartLockDemo.Business.Service.User;
using SmartLockDemo.Data;
using SmartLockDemo.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartLockDemo.Business.UnitTest.Service
{
    public class UserServiceTest
    {
        private readonly IUserService userService;

        private readonly Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IEncryptionUtilities> mockEncryptionUtilities = new Mock<IEncryptionUtilities>();

        public UserServiceTest()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            userService = BusinessModuleInitializer.Init(new List<ServiceDescriptor> {
                    ServiceDescriptor.Describe(typeof(IUnitOfWork), (serviceProvider) => mockUnitOfWork.Object, ServiceLifetime.Scoped),
                    ServiceDescriptor.Describe(typeof(IEncryptionUtilities), (serviceProvider) => mockEncryptionUtilities.Object, ServiceLifetime.Scoped)
            }).GetService<IUserService>();
        }

        [Fact]
        public void CreateUser_Return_Throws_Exception_If_Request_Is_Null()
        {
            // Arrange
            UserCreationRequest request = new();
            // Act and Assert
            Assert.Throws<Exception>(() => userService.CreateUser(request));
        }
    }
}
