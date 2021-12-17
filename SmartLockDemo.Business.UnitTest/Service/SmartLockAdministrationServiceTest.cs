using FluentValidation;
using Moq;
using SmartLockDemo.Business.Service.SmartLockAdministration;
using SmartLockDemo.Data;
using SmartLockDemo.Infrastructure.Utilities;
using System;
using Xunit;

namespace SmartLockDemo.Business.UnitTest.Service
{
    public class SmartLockAdministrationServiceTest
    {
        private const string ValidName = "Some Door";

        private ISmartLockAdministrationService smartLockAdministrationService = (new TestBusinessModuleInitializer())
            .GetService<ISmartLockAdministrationService>();

        [Fact]
        public void CreateDoor_Throws_ValidationException_If_Given_Request_Is_Null()
        {
            // Arrange
            DoorCreationRequest request = null;
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.CreateDoor(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void CreateDoor_Throws_ValidationException_If_Given_Name_Is_Null()
        {
            // Arrange
            DoorCreationRequest request = new();
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.CreateDoor(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Name"));
        }

        [Fact]
        public void CreateDoor_Throws_ValidationException_If_Given_Name_Is_Empty_String()
        {
            // Arrange
            DoorCreationRequest request = new DoorCreationRequest { Name = "" };
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.CreateDoor(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Name"));
        }

        [Fact]
        public void CreateDoor_Throws_ValidationException_If_Given_Name_Length_Is_More_Than_50()
        {
            // Arrange
            DoorCreationRequest request = new DoorCreationRequest { Name = new String('D', 51) };
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.CreateDoor(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Name"));
        }

        [Fact]
        public void CreateDoor_Throws_ValidationException_If_Given_Name_Is_Already_Exists()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.DoorRepository.CheckIfDoorAlreadyExists(ValidName))
                .Returns(true);
            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            ISmartLockAdministrationService administrationServiceToSetup = testModule.GetService<ISmartLockAdministrationService>();
            DoorCreationRequest request = new()
            {
                Name = ValidName
            };
            // Act
            Exception exception = Record.Exception(() => administrationServiceToSetup.CreateDoor(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("This door already exists!"));
        }

        [Fact]
        public void CreateDoor_Saves_A_New_Door_Entity_To_Door_Repository_If_Request_Is_Valid()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.DoorRepository.CheckIfDoorAlreadyExists(ValidName))
                .Returns(false);
            mockUnitOfWork.Setup(muw => muw.DoorRepository.Add(It.IsAny<Data.Entities.Door>()));
            mockUnitOfWork.Setup(muw => muw.SaveChanges());

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            ISmartLockAdministrationService administrationServiceToSetup = testModule.GetService<ISmartLockAdministrationService>();
            DoorCreationRequest validRequest = new()
            {
                Name = ValidName
            };
            // Act
            administrationServiceToSetup.CreateDoor(validRequest);
            // Assert
            mockUnitOfWork.Verify(muw =>
                muw.DoorRepository.Add(It.Is<Data.Entities.Door>(door =>
                    door.Name == ValidName)), Times.Once());
        }

        [Fact]
        public void CreateTag_Throws_ValidationException_If_Given_Request_Is_Null()
        {
            // Arrange
            TagCreationRequest request = null;
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.CreateTag(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void CreateTag_Throws_ValidationException_If_Given_Name_Is_Null()
        {
            // Arrange
            TagCreationRequest request = new();
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.CreateTag(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Name"));
        }

        [Fact]
        public void CreateTag_Throws_ValidationException_If_Given_Name_Is_Empty_String()
        {
            // Arrange
            TagCreationRequest request = new TagCreationRequest { Name = "" };
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.CreateTag(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Name"));
        }

        [Fact]
        public void CreateTag_Throws_ValidationException_If_Given_Name_Length_Is_More_Than_50()
        {
            // Arrange
            TagCreationRequest request = new TagCreationRequest { Name = new String('T', 51) };
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.CreateTag(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Name"));
        }

        [Fact]
        public void CreateTag_Throws_ValidationException_If_Given_Name_Is_Already_Exists()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.TagRepository.CheckIfTagAlreadyExists(ValidName))
                .Returns(true);
            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            ISmartLockAdministrationService administrationServiceToSetup = testModule.GetService<ISmartLockAdministrationService>();
            TagCreationRequest request = new()
            {
                Name = ValidName
            };
            // Act
            Exception exception = Record.Exception(() => administrationServiceToSetup.CreateTag(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("This tag already exists!"));
        }

        [Fact]
        public void CreateTag_Saves_A_New_Door_Entity_To_Door_Repository_If_Request_Is_Valid()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.TagRepository.CheckIfTagAlreadyExists(ValidName))
                .Returns(false);
            mockUnitOfWork.Setup(muw => muw.TagRepository.Add(It.IsAny<Data.Entities.Tag>()));
            mockUnitOfWork.Setup(muw => muw.SaveChanges());

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            ISmartLockAdministrationService administrationServiceToSetup = testModule.GetService<ISmartLockAdministrationService>();
            TagCreationRequest validRequest = new()
            {
                Name = ValidName
            };
            // Act
            administrationServiceToSetup.CreateTag(validRequest);
            // Assert
            mockUnitOfWork.Verify(muw =>
                muw.TagRepository.Add(It.Is<Data.Entities.Tag>(door =>
                    door.Name == ValidName)), Times.Once());
        }
    }
}
