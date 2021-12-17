﻿using FluentValidation;
using Moq;
using SmartLockDemo.Business.Service.User;
using SmartLockDemo.Data;
using SmartLockDemo.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartLockDemo.Business.UnitTest.Service
{
    public class UserServiceTest
    {
        private const string ValidEmail = "onurbpm@outlook.com";
        private const string ValidPassword = "Anonymouspassword123!";

        private IUserService userService = (new TestBusinessModuleInitializer()).GetService<IUserService>();

        [Fact]
        public void CreateUser_Throws_ValidationException_If_Request_Is_Null()
        {
            // Arrange
            UserCreationRequest request = null;
            // Act
            Exception exception = Record.Exception(() => userService.CreateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void CreateUser_Throws_ValidationException_If_Email_Is_Null()
        {
            // Arrange
            UserCreationRequest request = new UserCreationRequest
            {
                Email = null,
                Password = ValidPassword
            };
            // Act
            Exception exception = Record.Exception(() => userService.CreateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Email"));
        }

        [Fact]
        public void CreateUser_Throws_ValidationException_If_Email_Is_Empty_String()
        {
            // Arrange
            UserCreationRequest request = new UserCreationRequest
            {
                Email = "",
                Password = ValidPassword
            };
            // Act
            Exception exception = Record.Exception(() => userService.CreateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Email"));
        }

        [Fact]
        public void CreateUser_Throws_ValidationException_If_Email_Is_Not_Correct_Form()
        {
            // Arrange
            UserCreationRequest request = new UserCreationRequest
            {
                Email = "incorrectform@",
                Password = ValidPassword
            };
            // Act
            Exception exception = Record.Exception(() => userService.CreateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Email"));
        }

        [Fact]
        public void CreateUser_Throws_ValidationException_If_Email_Has_Character_More_Than_255()
        {
            // Arrange
            UserCreationRequest request = new UserCreationRequest
            {
                Email = $"{new String('a', 250)}@outlook.com",
                Password = ValidPassword
            };
            // Act
            Exception exception = Record.Exception(() => userService.CreateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Email"));
        }

        [Fact]
        public void CreateUser_Throws_ValidationException_If_Email_Is_Already_Exists()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfEmailAlreadyExists(ValidEmail))
                .Returns(true);
            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            IUserService userServiceToSetup = testModule.GetService<IUserService>();
            UserCreationRequest request = new()
            {
                Email = ValidEmail,
                Password = ValidPassword
            };
            // Act
            Exception exception = Record.Exception(() => userServiceToSetup.CreateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("This email already exists!"));
        }

        [Fact]
        public void CreateUser_Throws_ValidationException_If_Password_Is_Null()
        {
            // Arrange
            UserCreationRequest request = new UserCreationRequest
            {
                Email = ValidEmail,
                Password = null
            };
            // Act
            Exception exception = Record.Exception(() => userService.CreateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Password"));
        }

        [Fact]
        public void CreateUser_Throws_ValidationException_If_Password_Is_Empty()
        {
            // Arrange
            UserCreationRequest request = new UserCreationRequest
            {
                Email = ValidEmail,
                Password = ""
            };
            // Act
            Exception exception = Record.Exception(() => userService.CreateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Password"));
        }

        [Fact]
        public void CreateUser_Throws_ValidationException_If_Password_Is_Not_Strong()
        {
            // Arrange
            UserCreationRequest request = new UserCreationRequest
            {
                Email = ValidEmail,
                Password = "Anonymouspassword123"
            };
            // Act
            Exception exception = Record.Exception(() => userService.CreateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Password"));
        }


        [Fact]
        public void CreateUser_Throws_ValidationException_If_Password_Has_Character_More_Than_255()
        {
            // Arrange
            UserCreationRequest request = new UserCreationRequest
            {
                Email = ValidEmail,
                Password = $"{new String('a', 250)}{ValidPassword}"
            };
            // Act
            Exception exception = Record.Exception(() => userService.CreateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Password"));
        }

        [Fact]
        public void CreateUser_Saves_New_User_Entity_To_User_Repository()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw =>
                muw.UserRepository.Add(It.Is<Data.Entities.User>(user =>
                    user.Email == ValidEmail)));
            mockUnitOfWork.Setup(muw =>
                muw.SaveChanges());
            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            IUserService userServiceToSetup = testModule.GetService<IUserService>();
            UserCreationRequest request = new UserCreationRequest
            {
                Email = ValidEmail,
                Password = ValidPassword
            };
            // Act
            userServiceToSetup.CreateUser(request);
            // Assert
            mockUnitOfWork.Verify(muw =>
                muw.UserRepository.Add(It.Is<Data.Entities.User>(user =>
                    user.Email == ValidEmail)), Times.Once());
        }

        [Fact]
        public void CheckDoorAccess_Throws_ValidationException_If_Request_Is_Null()
        {
            // Arrange
            DoorAccessControlRequest request = null;
            // Act
            Exception exception = Record.Exception(() => userService.CheckDoorAccess(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void CheckDoorAccess_Throws_ValidationException_If_Given_UserId_Is_Not_Greater_Than_1()
        {
            // Arrange
            DoorAccessControlRequest request = new DoorAccessControlRequest { UserId = 0, DoorId = 1 };
            // Act
            Exception exception = Record.Exception(() => userService.CheckDoorAccess(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("UserId"));
        }

        [Fact]
        public void CheckDoorAccess_Throws_ValidationException_If_Given_DoorId_Is_Not_Greater_Than_1()
        {
            // Arrange
            DoorAccessControlRequest request = new DoorAccessControlRequest { UserId = 1, DoorId = 0 };
            // Act
            Exception exception = Record.Exception(() => userService.CheckDoorAccess(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("DoorId"));
        }

        [Fact]
        public void CheckDoorAccess_Returns_IsUserAuthorized_True_If_There_Are_Valid_Entities_In_TagDoor_And_UserTag_Repositories_By_Given_Request()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();

            SetupTagDoorRepository(mockUnitOfWork, new List<Data.Entities.TagDoor> {
                new Data.Entities.TagDoor {
                    DoorId = 1, TagId = 2
                }
            });
            SetupUserTagRepository(mockUnitOfWork, new List<Data.Entities.UserTag> {
                new Data.Entities.UserTag {
                    UserId = 1, TagId = 2
                }
            });

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            IUserService userServiceToSetup = testModule.GetService<IUserService>();

            DoorAccessControlRequest request = new()
            {
                UserId = 1,
                DoorId = 1,
            };

            // Act
            DoorAccessControlResult actualResult = userServiceToSetup.CheckDoorAccess(request);

            // Assert
            Assert.True(actualResult.IsUserAuthorized);
        }

        private static void SetupTagDoorRepository(Mock<IUnitOfWork> mockUnitOfWorkWillSetup, List<Data.Entities.TagDoor> mockRepository)
         => mockUnitOfWorkWillSetup.Setup(muw => muw.TagDoorRepository.GetTable())
                .Returns(mockRepository.AsQueryable());

        private static void SetupUserTagRepository(Mock<IUnitOfWork> mockUnitOfWorkWillSetup, List<Data.Entities.UserTag> mockRepository)
            => mockUnitOfWorkWillSetup.Setup(muw => muw.UserTagRepository.GetTable())
                .Returns(mockRepository.AsQueryable());

        [Fact]
        public void CheckDoorAccess_Returns_IsUserAuthorized_False_If_There_Are_No_Valid_Entitie_In_TagDoor_And_UserTag_Repositories_By_Given_Request()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();

            SetupTagDoorRepository(mockUnitOfWork, new List<Data.Entities.TagDoor> {
                new Data.Entities.TagDoor {
                    DoorId = 1, TagId = 2
                }
            });
            SetupUserTagRepository(mockUnitOfWork, new List<Data.Entities.UserTag> {
                new Data.Entities.UserTag {
                     UserId = 1, TagId = 1
                },
                new Data.Entities.UserTag {
                     UserId = 1, TagId = 3
                }
            });

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            IUserService userServiceToSetup = testModule.GetService<IUserService>();

            DoorAccessControlRequest request = new()
            {
                UserId = 3,
                DoorId = 1,
            };

            // Act
            DoorAccessControlResult actualResult = userServiceToSetup.CheckDoorAccess(request);

            // Assert
            Assert.False(actualResult.IsUserAuthorized);
        }
    }
}