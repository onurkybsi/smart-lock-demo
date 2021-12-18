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
        private const string ValidName = "ValidName";

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

        [Fact]
        public void CreateDoorAccess_Throws_ValidationException_If_Given_Request_Is_Null()
        {
            // Arrange
            DoorAccessCreationRequest request = null;
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.CreateDoorAccess(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void CreateDoorAccess_Throws_ValidationException_If_Given_DoorId_Is_Less_Than_1()
        {
            // Arrange
            DoorAccessCreationRequest request = new DoorAccessCreationRequest { DoorId = 0, TagId = 2 };
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.CreateDoorAccess(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("DoorId"));
        }

        [Fact]
        public void CreateDoorAccess_Throws_ValidationException_If_Given_TagId_Is_Less_Than_1()
        {
            // Arrange
            DoorAccessCreationRequest request = new DoorAccessCreationRequest { DoorId = 2, TagId = 0 };
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.CreateDoorAccess(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("TagId"));
        }

        [Fact]
        public void CreateDoorAccess_Throws_ValidationException_If_There_Is_No_Door_In_DoorRepository_By_Given_DoorId()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.DoorRepository.CheckIfDoorAlreadyExists(1))
                .Returns(false);
            mockUnitOfWork.Setup(muw => muw.TagRepository.CheckIfTagAlreadyExists(1))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.TagDoorRepository.CheckIfAccessAlreadyExistsForThisTag(2, 2))
                .Returns(false);

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            ISmartLockAdministrationService administrationServiceToSetup = testModule.GetService<ISmartLockAdministrationService>();

            DoorAccessCreationRequest request = new DoorAccessCreationRequest { DoorId = 2, TagId = 2 };
            // Act
            Exception exception = Record.Exception(() => administrationServiceToSetup.CreateDoorAccess(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("DoorId"));
        }

        [Fact]
        public void CreateDoorAccess_Throws_ValidationException_If_There_Is_No_Tag_In_TagRepository_By_Given_TagId()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.DoorRepository.CheckIfDoorAlreadyExists(1))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.TagRepository.CheckIfTagAlreadyExists(1))
                .Returns(false);
            mockUnitOfWork.Setup(muw => muw.TagDoorRepository.CheckIfAccessAlreadyExistsForThisTag(2, 2))
                .Returns(false);

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            ISmartLockAdministrationService administrationServiceToSetup = testModule.GetService<ISmartLockAdministrationService>();

            DoorAccessCreationRequest request = new DoorAccessCreationRequest { DoorId = 2, TagId = 2 };
            // Act
            Exception exception = Record.Exception(() => administrationServiceToSetup.CreateDoorAccess(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("TagId"));
        }

        [Fact]
        public void CreateDoorAccess_Throws_ValidationException_If_There_Is_Already_Access_Definition_In_TagDoor_Repository_By_Given_Request()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.DoorRepository.CheckIfDoorAlreadyExists(1))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.TagRepository.CheckIfTagAlreadyExists(1))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.TagDoorRepository.CheckIfAccessAlreadyExistsForThisTag(2, 2))
                .Returns(true);

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            ISmartLockAdministrationService administrationServiceToSetup = testModule.GetService<ISmartLockAdministrationService>();

            DoorAccessCreationRequest request = new DoorAccessCreationRequest { DoorId = 2, TagId = 2 };
            // Act
            Exception exception = Record.Exception(() => administrationServiceToSetup.CreateDoorAccess(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("This tag has already access to this door!"));
        }

        [Fact]
        public void CreateDoorAccess_Creates_New_Access_For_A_Tag_To_A_Door_If_Request_Is_Valid()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.DoorRepository.CheckIfDoorAlreadyExists(2))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.TagRepository.CheckIfTagAlreadyExists(2))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.TagDoorRepository.CheckIfAccessAlreadyExistsForThisTag(2, 2))
                .Returns(false);
            mockUnitOfWork.Setup(muw => muw.TagDoorRepository.Add(It.IsAny<Data.Entities.TagDoor>()));
            mockUnitOfWork.Setup(muw => muw.SaveChanges());

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            ISmartLockAdministrationService administrationServiceToSetup = testModule.GetService<ISmartLockAdministrationService>();

            DoorAccessCreationRequest request = new DoorAccessCreationRequest { DoorId = 2, TagId = 2 };
            // Act
            administrationServiceToSetup.CreateDoorAccess(request);
            // Assert
            mockUnitOfWork.Verify(muw =>
                muw.TagDoorRepository.Add(It.Is<Data.Entities.TagDoor>(tagDoor =>
                    tagDoor.TagId == 2 && tagDoor.DoorId == 2)), Times.Once());
        }

        [Fact]
        public void TagUser_Throws_ValidationException_If_Given_Request_Is_Null()
        {
            // Arrange
            UserTaggingRequest request = null;
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.TagUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void TagUser_Throws_ValidationException_If_Given_UserId_Is_Less_Than_1()
        {
            // Arrange
            UserTaggingRequest request = new UserTaggingRequest { UserId = 0, TagId = 2 };
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.TagUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("UserId"));
        }

        [Fact]
        public void TagUser_Throws_ValidationException_If_Given_TagId_Is_Less_Than_1()
        {
            // Arrange
            UserTaggingRequest request = new UserTaggingRequest { UserId = 2, TagId = 0 };
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.TagUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("TagId"));
        }

        [Fact]
        public void TagUser_Throws_ValidationException_If_There_Is_No_User_In_UserRepository_By_Given_UserId()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfUserExistsOrNot(2))
                .Returns(false);
            mockUnitOfWork.Setup(muw => muw.TagRepository.CheckIfTagAlreadyExists(2))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.UserTagRepository.CheckIfUserAlreadyHasThisTag(2, 2))
                .Returns(false);

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            ISmartLockAdministrationService administrationServiceToSetup = testModule.GetService<ISmartLockAdministrationService>();

            UserTaggingRequest request = new UserTaggingRequest { UserId = 2, TagId = 2 };
            // Act
            Exception exception = Record.Exception(() => administrationServiceToSetup.TagUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("UserId"));
        }

        [Fact]
        public void TagUser_Throws_ValidationException_If_There_Is_No_Tag_In_TagRepository_By_Given_TagId()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfUserExistsOrNot(1))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.TagRepository.CheckIfTagAlreadyExists(1))
                .Returns(false);
            mockUnitOfWork.Setup(muw => muw.UserTagRepository.CheckIfUserAlreadyHasThisTag(2, 2))
                .Returns(false);

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            ISmartLockAdministrationService administrationServiceToSetup = testModule.GetService<ISmartLockAdministrationService>();

            UserTaggingRequest request = new UserTaggingRequest { UserId = 2, TagId = 2 };
            // Act
            Exception exception = Record.Exception(() => administrationServiceToSetup.TagUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("TagId"));
        }

        [Fact]
        public void TagUser_Throws_ValidationException_If_User_Is_Already_Has_This_Tag()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfUserExistsOrNot(2))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.TagRepository.CheckIfTagAlreadyExists(2))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.UserTagRepository.CheckIfUserAlreadyHasThisTag(2, 2))
                .Returns(true);

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            ISmartLockAdministrationService administrationServiceToSetup = testModule.GetService<ISmartLockAdministrationService>();

            UserTaggingRequest request = new UserTaggingRequest { UserId = 2, TagId = 2 };
            // Act
            Exception exception = Record.Exception(() => administrationServiceToSetup.TagUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("This user has already this tag!"));
        }

        [Fact]
        public void TagUser_Tags_User_To_It_Can_Access_Doors_That_Is_Accessible_With_That_Tag()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfUserExistsOrNot(2))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.TagRepository.CheckIfTagAlreadyExists(2))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.UserTagRepository.CheckIfUserAlreadyHasThisTag(2, 2))
                .Returns(false);
            mockUnitOfWork.Setup(muw => muw.UserTagRepository.Add(It.IsAny<Data.Entities.UserTag>()));
            mockUnitOfWork.Setup(muw => muw.SaveChanges());

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            ISmartLockAdministrationService administrationServiceToSetup = testModule.GetService<ISmartLockAdministrationService>();

            UserTaggingRequest request = new UserTaggingRequest { UserId = 2, TagId = 2 };
            // Act
            administrationServiceToSetup.TagUser(request);
            // Assert
            mockUnitOfWork.Verify(muw =>
                muw.UserTagRepository.Add(It.Is<Data.Entities.UserTag>(userTag =>
                    userTag.TagId == 2 && userTag.UserId == 2)), Times.Once());
        }

        [Fact]
        public void RemoveDoorAccess_Throws_ValidationException_If_Given_Request_Is_Null()
        {
            // Arrange
            DoorAccessRemovalRequest request = null;
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.RemoveDoorAccess(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void RemoveDoorAccess_Throws_ValidationException_If_Given_TagId_Is_Less_Than_1()
        {
            // Arrange
            DoorAccessRemovalRequest request = new() { TagId = 0, DoorId = 2 };
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.RemoveDoorAccess(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("TagId"));
        }

        [Fact]
        public void RemoveDoorAccess_Throws_ValidationException_If_Given_DoorId_Is_Less_Than_1()
        {
            // Arrange
            DoorAccessRemovalRequest request = new() { DoorId = 0, TagId = 2 };
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.RemoveDoorAccess(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("DoorId"));
        }

        [Fact]
        public void RemoveDoorAccess_Throws_ValidationException_If_There_Is_Already_No_Access_By_Given_Parameters()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.TagDoorRepository.CheckIfAccessAlreadyExistsForThisTag(2, 2))
                .Returns(false);

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            ISmartLockAdministrationService administrationServiceToSetup = testModule.GetService<ISmartLockAdministrationService>();

            DoorAccessRemovalRequest request = new() { DoorId = 2, TagId = 2 };
            // Act
            Exception exception = Record.Exception(() => administrationServiceToSetup.RemoveDoorAccess(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("There is no such an access!"));
        }

        [Fact]
        public void RemoveDoorAccess_Removes_TagDoor_Entity_From_TagDoor_Repository_By_Given_Parameters_If_Request_Is_Valid()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.TagDoorRepository.CheckIfAccessAlreadyExistsForThisTag(2, 2))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.TagDoorRepository.Remove(2, 2));
            mockUnitOfWork.Setup(muw => muw.SaveChanges());

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            ISmartLockAdministrationService administrationServiceToSetup = testModule.GetService<ISmartLockAdministrationService>();

            DoorAccessRemovalRequest request = new() { DoorId = 2, TagId = 2 };
            // Act
            administrationServiceToSetup.RemoveDoorAccess(request);
            // Assert
            mockUnitOfWork.Verify(muw => muw.TagDoorRepository.Remove(2, 2), Times.Once);
        }

        [Fact]
        public void RemoveUserTag_Throws_ValidationException_If_Given_Request_Is_Null()
        {
            // Arrange
            UserTagRemovalRequest request = null;
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.RemoveUserTag(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void RemoveUserTag_Throws_ValidationException_If_Given_TagId_Is_Less_Than_1()
        {
            // Arrange
            UserTagRemovalRequest request = new() { TagId = 0, UserId = 2 };
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.RemoveUserTag(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("TagId"));
        }

        [Fact]
        public void RemoveUserTag_Throws_ValidationException_If_Given_UserId_Is_Less_Than_1()
        {
            // Arrange
            UserTagRemovalRequest request = new() { TagId = 2, UserId = 0 };
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.RemoveUserTag(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("UserId"));
        }

        [Fact]
        public void RemoveUserTag_Throws_ValidationException_If_User_Already_Has_Not_Tag_In_Given_Parameters()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserTagRepository.CheckIfUserAlreadyHasThisTag(2, 2))
                .Returns(false);

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            ISmartLockAdministrationService administrationServiceToSetup = testModule.GetService<ISmartLockAdministrationService>();

            UserTagRemovalRequest request = new() { UserId = 2, TagId = 2 };
            // Act
            Exception exception = Record.Exception(() => administrationServiceToSetup.RemoveUserTag(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("There is no such a tag on this user!"));
        }

        [Fact]
        public void RemoveUserTag_Removes_TagDoor_Entity_From_TagDoor_Repository_By_Given_Parameters_If_Request_Is_Valid()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserTagRepository.CheckIfUserAlreadyHasThisTag(2, 2))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.UserTagRepository.Remove(2, 2));
            mockUnitOfWork.Setup(muw => muw.SaveChanges());

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            ISmartLockAdministrationService administrationServiceToSetup = testModule.GetService<ISmartLockAdministrationService>();

            UserTagRemovalRequest request = new() { UserId = 2, TagId = 2 };
            // Act
            administrationServiceToSetup.RemoveUserTag(request);
            // Assert
            mockUnitOfWork.Verify(muw => muw.UserTagRepository.Remove(2, 2), Times.Once);
        }

        [Fact]
        public void DeleteUser_Throws_ValidationException_If_Given_Request_Is_Null()
        {
            // Arrange
            UserDeletionRequest request = null;
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.DeleteUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void DeleteUser_Throws_ValidationException_If_Given_UserId_Is_Less_Than_1()
        {
            // Arrange
            UserDeletionRequest request = new() { UserId = 0 };
            // Act
            Exception exception = Record.Exception(() => smartLockAdministrationService.DeleteUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("UserId"));
        }

        [Fact]
        public void DeleteUser_Throws_ValidationException_If_User_Already_Has_Not_Tag_In_Given_Parameters()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfUserExistsOrNot(2))
                .Returns(false);

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            ISmartLockAdministrationService administrationServiceToSetup = testModule.GetService<ISmartLockAdministrationService>();

            UserDeletionRequest request = new() { UserId = 2 };
            // Act
            Exception exception = Record.Exception(() => administrationServiceToSetup.DeleteUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("There is no such a user already!"));
        }

        [Fact]
        public void DeleteUser_Deletes_User_Entity_From_UserRepository_By_Given_Parameters_If_Request_Is_Valid()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.UserRepository.CheckIfUserExistsOrNot(2))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.UserRepository.Delete(2));
            mockUnitOfWork.Setup(muw => muw.SaveChanges());

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            ISmartLockAdministrationService administrationServiceToSetup = testModule.GetService<ISmartLockAdministrationService>();

            UserDeletionRequest request = new() { UserId = 2 };
            // Act
            administrationServiceToSetup.DeleteUser(request);
            // Assert
            mockUnitOfWork.Verify(muw => muw.UserRepository.Delete(2), Times.Once);
        }
    }
}
