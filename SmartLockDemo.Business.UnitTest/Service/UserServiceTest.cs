using FluentValidation;
using Moq;
using SmartLockDemo.Business.Service.User;
using SmartLockDemo.Data;
using SmartLockDemo.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace SmartLockDemo.Business.UnitTest.Service
{
    public class UserServiceTest
    {
        private const string ValidEmail = "onurbpm@outlook.com";
        private const string ValidPassword = "Anonymouspassword123!";

        private IUserService userService = (new TestBusinessModuleInitializer()).GetService<IUserService>();

        [Fact]
        public void CreateUser_Throws_ValidationException_If_Given_Request_Is_Null()
        {
            // Arrange
            UserCreationRequest request = null;
            // Act
            Exception exception = Record.Exception(() => userService.CreateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void CreateUser_Throws_ValidationException_If_Given_Email_Is_Null()
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
        public void CreateUser_Throws_ValidationException_If_Given_Email_Is_Empty_String()
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
        public void CreateUser_Throws_ValidationException_If_Given_Email_Is_Not_Correct_Form()
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
        public void CreateUser_Throws_ValidationException_If_Given_Email_Has_Character_More_Than_255()
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
        public void CreateUser_Throws_ValidationException_If_Given_Email_Is_Already_Exists()
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
        public void CreateUser_Throws_ValidationException_If_Given_Password_Is_Null()
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
        public void CreateUser_Throws_ValidationException_If_Given_Password_Is_Empty()
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
        public void CreateUser_Throws_ValidationException_If_Given_Password_Is_Not_Strong()
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
        public void CreateUser_Throws_ValidationException_If_Given_Password_Has_Character_More_Than_255()
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
        public void CreateUser_Saves_A_New_User_Entity_To_User_Repository()
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
        public void CheckDoorAccess_Throws_ValidationException_If_Given_Request_Is_Null()
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

        [Fact]
        public void UpdateUser_Throws_ValidationException_If_Given_Request_Is_Null()
        {
            // Arrange
            UserUpdateRequest request = null;
            // Act
            Exception exception = Record.Exception(() => userService.UpdateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void UpdateUser_Throws_ValidationException_If_Given_Id_Is_Null()
        {
            // Arrange
            UserUpdateRequest request = new();
            // Act
            Exception exception = Record.Exception(() => userService.UpdateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Id"));
        }

        [Fact]
        public void UpdateUser_Throws_ValidationException_If_Given_Id_Is_Less_Than_1()
        {
            // Arrange
            UserUpdateRequest request = new() { Id = 0 };
            // Act
            Exception exception = Record.Exception(() => userService.UpdateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Id"));
        }

        [Fact]
        public void UpdateUser_Throws_ValidationException_If_There_Is_No_Any_User_By_Given_Id()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfUserExistsOrNot(1))
                .Returns(false);
            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            IUserService userServiceToSetup = testModule.GetService<IUserService>();
            UserUpdateRequest request = new() { Id = 1 };
            // Act
            Exception exception = Record.Exception(() => userServiceToSetup.UpdateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("There is no such a user!"));
        }

        [Fact]
        public void UpdateUser_Throws_ValidationException_If_Given_Email_Is_Not_In_Email_Format()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfUserExistsOrNot(1))
                .Returns(true);
            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            IUserService userServiceToSetup = testModule.GetService<IUserService>();
            UserUpdateRequest request = new() { Id = 1, Email = "InvalidForm" };
            // Act
            Exception exception = Record.Exception(() => userServiceToSetup.UpdateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Email"));
        }

        [Fact]
        public void UpdateUser_Throws_ValidationException_If_Given_Email_Length_Is_More_Than_255()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfUserExistsOrNot(1))
                .Returns(true);
            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            IUserService userServiceToSetup = testModule.GetService<IUserService>();
            UserUpdateRequest request = new() { Id = 1, Email = new String('E', 256) };
            // Act
            Exception exception = Record.Exception(() => userServiceToSetup.UpdateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Email"));
        }

        [Fact]
        public void UpdateUser_Throws_ValidationException_If_Given_Email_Is_Already_Exists()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfUserExistsOrNot(1))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfEmailAlreadyExists(ValidEmail))
                .Returns(true);
            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            IUserService userServiceToSetup = testModule.GetService<IUserService>();
            UserUpdateRequest request = new() { Id = 1, Email = ValidEmail };
            // Act
            Exception exception = Record.Exception(() => userServiceToSetup.UpdateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Email"));
        }

        [Fact]
        public void UpdateUser_Throws_ValidationException_If_Given_Password_Is_Not_Strong()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfUserExistsOrNot(1))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfEmailAlreadyExists(ValidEmail))
                .Returns(false);
            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            IUserService userServiceToSetup = testModule.GetService<IUserService>();
            UserUpdateRequest request = new() { Id = 1, Email = ValidEmail, Password = "invalidPassword" };
            // Act
            Exception exception = Record.Exception(() => userServiceToSetup.UpdateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Password"));
        }

        [Fact]
        public void UpdateUser_Throws_ValidationException_If_Given_Password_Length_Is_More_Than_255()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfUserExistsOrNot(1))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfEmailAlreadyExists(ValidEmail))
                .Returns(false);
            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            IUserService userServiceToSetup = testModule.GetService<IUserService>();
            UserUpdateRequest request = new() { Id = 1, Email = ValidEmail, Password = new String('P', 256) };
            // Act
            Exception exception = Record.Exception(() => userServiceToSetup.UpdateUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Password"));
        }

        [Fact]
        public void UpdateUser_Does_Not_Throw_ValidationException_If_Given_Email_And_Password_Null_And_Password_Is_Valid()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfUserExistsOrNot(1))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.UserRepository.Update(It.Is<Data.Entities.User>(user =>
                user.Id == 1))
            );
            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            IUserService userServiceToSetup = testModule.GetService<IUserService>();
            UserUpdateRequest request = new() { Id = 1 };
            // Act
            Exception exception = Record.Exception(() => userServiceToSetup.UpdateUser(request));
            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void UpdateUser_Updates_User_If_Request_Is_Valid()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfUserExistsOrNot(1))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.UserRepository.Update(It.Is<Data.Entities.User>(user =>
                user.Id == 1))
            );
            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            IUserService userServiceToSetup = testModule.GetService<IUserService>();
            UserUpdateRequest request = new() { Id = 1 };
            // Act
            Exception exception = Record.Exception(() => userServiceToSetup.UpdateUser(request));
            // Assert
            mockUnitOfWork.Verify(muw => muw.UserRepository.Update(It.Is<Data.Entities.User>(user =>
                user.Id == 1)), Times.Once()
            );
        }

        [Fact]
        public void LogIn_Throws_ValidationException_If_Given_Request_Is_Null()
        {
            // Arrange
            LogInRequest request = null;
            // Act
            Exception exception = Record.Exception(() => userService.LogIn(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void LogIn_Throws_ValidationException_If_Given_Email_Is_Null()
        {
            // Arrange
            LogInRequest request = new() { Password = ValidPassword };
            // Act
            Exception exception = Record.Exception(() => userService.LogIn(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Email"));
        }

        [Fact]
        public void LogIn_Throws_ValidationException_If_Given_Email_Is_Empty_String()
        {
            // Arrange
            LogInRequest request = new() { Email = "", Password = ValidPassword };
            // Act
            Exception exception = Record.Exception(() => userService.LogIn(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Email"));
        }

        [Fact]
        public void LogIn_Throws_ValidationException_If_Given_Email_Length_Is_Greater_Than_255()
        {
            // Arrange
            LogInRequest request = new() { Email = new String('E', 256), Password = ValidPassword };
            // Act
            Exception exception = Record.Exception(() => userService.LogIn(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Email"));
        }

        [Fact]
        public void LogIn_Throws_ValidationException_If_Given_Email_Is_Not_In_Correct_Format()
        {
            // Arrange
            LogInRequest request = new() { Email = "incorrectformat@something", Password = ValidPassword };
            // Act
            Exception exception = Record.Exception(() => userService.LogIn(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Email"));
        }

        [Fact]
        public void LogIn_Throws_ValidationException_If_Given_Email_Is_Not_Exist_In_UserRepository()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfEmailAlreadyExists(ValidEmail))
                .Returns(false);

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            IUserService userServiceToSetup = testModule.GetService<IUserService>();

            LogInRequest request = new() { Email = ValidEmail, Password = ValidPassword };
            // Act
            Exception exception = Record.Exception(() => userServiceToSetup.LogIn(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("There is no such an email!"));
        }

        [Fact]
        public void LogIn_Throws_ValidationException_If_Given_Password_Is_Null()
        {
            // Arrange
            LogInRequest request = new() { Email = ValidEmail };
            // Act
            Exception exception = Record.Exception(() => userService.LogIn(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Password"));
        }

        [Fact]
        public void LogIn_Throws_ValidationException_If_Given_Password_Is_Empty_String()
        {
            // Arrange
            LogInRequest request = new() { Password = "", Email = ValidEmail };
            // Act
            Exception exception = Record.Exception(() => userService.LogIn(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Password"));
        }

        [Fact]
        public void LogIn_Throws_ValidationException_If_Given_Password_Is_Not_Strong()
        {
            // Arrange
            LogInRequest request = new() { Password = "nonstrongpass", Email = ValidEmail };
            // Act
            Exception exception = Record.Exception(() => userService.LogIn(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Password"));
        }

        [Fact]
        public void LogIn_Throws_ValidationException_If_Given_Password_Length_Is_Greater_Than_255()
        {
            // Arrange
            LogInRequest request = new() { Email = ValidEmail, Password = new String('P', 256) };
            // Act
            Exception exception = Record.Exception(() => userService.LogIn(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Password"));
        }

        [Fact]
        public void LogIn_Returns_IsSuccessful_False_If_Given_Password_Is_Wrong()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfEmailAlreadyExists(ValidEmail))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.UserRepository.Get(It.IsAny<Expression<Func<Data.Entities.User, bool>>>()))
                .Returns(new Data.Entities.User { HashedPassword = "AnotherPassword123!" });

            Mock<IEncryptionUtilities> mockEncryptionUtilities = new();
            mockEncryptionUtilities.Setup(meu => meu.ValidateHashedValue(ValidEmail, "AnotherPassword123!"))
                .Returns(false);

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, mockEncryptionUtilities.Object);
            IUserService userServiceToSetup = testModule.GetService<IUserService>();

            LogInRequest request = new() { Email = ValidEmail, Password = ValidPassword };
            // Act
            LogInResult actualResult = userServiceToSetup.LogIn(request);
            // Assert
            Assert.False(actualResult.IsSuccessful);
        }

        [Fact]
        public void LogIn_Updates_UserRepository_By_CreatedToken()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfEmailAlreadyExists(ValidEmail))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.UserRepository.Get(It.IsAny<Expression<Func<Data.Entities.User, bool>>>()))
                .Returns(new Data.Entities.User { Id = 1, Email = ValidEmail, Role = (byte)Role.User, HashedPassword = "HashedPassword" });
            mockUnitOfWork.Setup(muw =>
                muw.UserRepository.Update(It.Is<Data.Entities.User>(user => user.AuthorizationToken == "CreatedToken")));
            mockUnitOfWork.Setup(muw => muw.SaveChanges());

            Mock<IEncryptionUtilities> mockEncryptionUtilities = new();
            mockEncryptionUtilities.Setup(meu => meu.ValidateHashedValue(ValidPassword, "HashedPassword"))
                .Returns(true);
            mockEncryptionUtilities.Setup(meu => meu.CreateBearerToken(It.Is<BearerTokenCreationRequest>(req =>
                req.Id == 1 && req.Email == ValidEmail && req.Role == Role.User.ToString()))
            ).Returns("CreatedToken");

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, mockEncryptionUtilities.Object);
            IUserService userServiceToSetup = testModule.GetService<IUserService>();

            LogInRequest request = new() { Email = ValidEmail, Password = ValidPassword };
            // Act
            LogInResult actualResult = userServiceToSetup.LogIn(request);
            // Assert
            mockUnitOfWork.Verify(muw =>
                muw.UserRepository.Update(It.Is<Data.Entities.User>(user => user.AuthorizationToken == "CreatedToken")), Times.Once());
        }

        [Fact]
        public void LogIn_Returns_True_If_User_Specified_In_Given_Request_Is_Authenticated()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfEmailAlreadyExists(ValidEmail))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.UserRepository.Get(It.IsAny<Expression<Func<Data.Entities.User, bool>>>()))
                .Returns(new Data.Entities.User { Id = 1, Email = ValidEmail, Role = (byte)Role.User, HashedPassword = "HashedPassword" });
            mockUnitOfWork.Setup(muw =>
                muw.UserRepository.Update(It.Is<Data.Entities.User>(user => user.AuthorizationToken == "CreatedToken")));
            mockUnitOfWork.Setup(muw => muw.SaveChanges());

            Mock<IEncryptionUtilities> mockEncryptionUtilities = new();
            mockEncryptionUtilities.Setup(meu => meu.ValidateHashedValue(ValidPassword, "HashedPassword"))
                .Returns(true);
            mockEncryptionUtilities.Setup(meu => meu.CreateBearerToken(It.Is<BearerTokenCreationRequest>(req =>
                req.Id == 1 && req.Email == ValidEmail && req.Role == Role.User.ToString()))
            ).Returns("CreatedToken");

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, mockEncryptionUtilities.Object);
            IUserService userServiceToSetup = testModule.GetService<IUserService>();

            LogInRequest request = new() { Email = ValidEmail, Password = ValidPassword };
            // Act
            LogInResult actualResult = userServiceToSetup.LogIn(request);
            // Assert
            Assert.True(actualResult.IsSuccessful);
        }

        [Fact]
        public void LogIn_Returns_CreatedToken_If_User_Specified_In_Given_Request_Is_Authenticated()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfEmailAlreadyExists(ValidEmail))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.UserRepository.Get(It.IsAny<Expression<Func<Data.Entities.User, bool>>>()))
                .Returns(new Data.Entities.User { Id = 1, Email = ValidEmail, Role = (byte)Role.User, HashedPassword = "HashedPassword" });
            mockUnitOfWork.Setup(muw =>
                muw.UserRepository.Update(It.Is<Data.Entities.User>(user => user.AuthorizationToken == "CreatedToken")));
            mockUnitOfWork.Setup(muw => muw.SaveChanges());

            Mock<IEncryptionUtilities> mockEncryptionUtilities = new();
            mockEncryptionUtilities.Setup(meu => meu.ValidateHashedValue(ValidPassword, "HashedPassword"))
                .Returns(true);
            mockEncryptionUtilities.Setup(meu => meu.CreateBearerToken(It.Is<BearerTokenCreationRequest>(req =>
                req.Id == 1 && req.Email == ValidEmail && req.Role == Role.User.ToString()))
            ).Returns("CreatedToken");

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, mockEncryptionUtilities.Object);
            IUserService userServiceToSetup = testModule.GetService<IUserService>();

            LogInRequest request = new() { Email = ValidEmail, Password = ValidPassword };
            // Act
            LogInResult actualResult = userServiceToSetup.LogIn(request);
            // Assert
            Assert.Equal("CreatedToken", actualResult.CreatedToken);
        }

        [Fact]
        public void CheckIfUserIsAdmin_Returns_False_If_Given_UserId_Less_Or_Equal_To_Zero()
        {
            // Act and Assert
            Assert.False(userService.CheckIfUserIsAdmin(0));
        }
    }
}