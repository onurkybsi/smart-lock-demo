using FluentValidation;
using Moq;
using SmartLockDemo.Business.Service.User;
using System;
using Xunit;

namespace SmartLockDemo.Business.UnitTest.Service
{
    public class UserServiceTest
    {
        private const string ValidEmail = "onurbpm@outlook.com";
        private const string ValidPassword = "Anonymouspassword123!";

        private IUserService userService = (new TestBusinessModuleInitializer()).GetService<IUserService>();

        [Fact]
        public void CreateUser_Throws_ArgumentNullException_If_Request_Is_Null()
        {
            // Arrange
            UserCreationRequest request = null;
            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => userService.CreateUser(request));
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
            TestBusinessModuleInitializer testModule = new();
            IUserService userServiceToSetup = testModule.GetService<IUserService>();
            testModule.GetMockUnitOfWork().Setup(muw =>
                muw.UserRepository.Add(It.Is<Data.Entites.User>(user =>
                    user.Email == ValidEmail)));
            testModule.GetMockUnitOfWork().Setup(muw =>
                muw.SaveChanges());
            UserCreationRequest request = new UserCreationRequest
            {
                Email = ValidEmail,
                Password = ValidPassword
            };
            // Act
            userServiceToSetup.CreateUser(request);
            // Assert
            testModule.GetMockUnitOfWork().Verify(muw =>
                muw.UserRepository.Add(It.Is<Data.Entites.User>(user =>
                    user.Email == ValidEmail)), Times.Once());
        }

        [Fact]
        public void CreateUser_Returns_Successful_User_Creation_Result()
        {
            // Arrange
            TestBusinessModuleInitializer testModule = new();
            IUserService userServiceToSetup = testModule.GetService<IUserService>();
            testModule.GetMockUnitOfWork().Setup(muw =>
                muw.UserRepository.Add(It.Is<Data.Entites.User>(user =>
                    user.Email == ValidEmail)));
            testModule.GetMockUnitOfWork().Setup(muw =>
                muw.SaveChanges());
            UserCreationRequest request = new UserCreationRequest
            {
                Email = ValidEmail,
                Password = ValidPassword
            };
            // Act
            var result = userServiceToSetup.CreateUser(request);
            // Assert
            Assert.True(result.IsSuccessful);
        }
    }
}
