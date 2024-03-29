﻿using FluentValidation;
using Moq;
using SmartLockDemo.Business.Service.Administration;
using SmartLockDemo.Data;
using SmartLockDemo.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace SmartLockDemo.Business.UnitTest.Service
{
    public class AdministrationServiceTest
    {
        private const string ValidName = "ValidName";

        private IAdministrationService administrationService = (new TestBusinessModuleInitializer())
            .GetService<IAdministrationService>();

        [Fact]
        public void CreateDoor_Throws_ValidationException_If_Given_Request_Is_Null()
        {
            // Arrange
            DoorCreationRequest request = null;
            // Act
            Exception exception = Record.Exception(() => administrationService.CreateDoor(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void CreateDoor_Throws_ValidationException_If_Given_Name_Is_Null()
        {
            // Arrange
            DoorCreationRequest request = new();
            // Act
            Exception exception = Record.Exception(() => administrationService.CreateDoor(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Name"));
        }

        [Fact]
        public void CreateDoor_Throws_ValidationException_If_Given_Name_Is_Empty_String()
        {
            // Arrange
            DoorCreationRequest request = new DoorCreationRequest { Name = "" };
            // Act
            Exception exception = Record.Exception(() => administrationService.CreateDoor(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Name"));
        }

        [Fact]
        public void CreateDoor_Throws_ValidationException_If_Given_Name_Length_Is_More_Than_50()
        {
            // Arrange
            DoorCreationRequest request = new DoorCreationRequest { Name = new String('D', 51) };
            // Act
            Exception exception = Record.Exception(() => administrationService.CreateDoor(request));
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
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();
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
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();
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
            Exception exception = Record.Exception(() => administrationService.CreateTag(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void CreateTag_Throws_ValidationException_If_Given_Name_Is_Null()
        {
            // Arrange
            TagCreationRequest request = new();
            // Act
            Exception exception = Record.Exception(() => administrationService.CreateTag(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Name"));
        }

        [Fact]
        public void CreateTag_Throws_ValidationException_If_Given_Name_Is_Empty_String()
        {
            // Arrange
            TagCreationRequest request = new TagCreationRequest { Name = "" };
            // Act
            Exception exception = Record.Exception(() => administrationService.CreateTag(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Name"));
        }

        [Fact]
        public void CreateTag_Throws_ValidationException_If_Given_Name_Length_Is_More_Than_50()
        {
            // Arrange
            TagCreationRequest request = new TagCreationRequest { Name = new String('T', 51) };
            // Act
            Exception exception = Record.Exception(() => administrationService.CreateTag(request));
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
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();
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
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();
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
            Exception exception = Record.Exception(() => administrationService.CreateDoorAccess(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void CreateDoorAccess_Throws_ValidationException_If_Given_DoorId_Is_Less_Than_1()
        {
            // Arrange
            DoorAccessCreationRequest request = new DoorAccessCreationRequest { DoorId = 0, TagId = 2 };
            // Act
            Exception exception = Record.Exception(() => administrationService.CreateDoorAccess(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("DoorId"));
        }

        [Fact]
        public void CreateDoorAccess_Throws_ValidationException_If_Given_TagId_Is_Less_Than_1()
        {
            // Arrange
            DoorAccessCreationRequest request = new DoorAccessCreationRequest { DoorId = 2, TagId = 0 };
            // Act
            Exception exception = Record.Exception(() => administrationService.CreateDoorAccess(request));
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
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();

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
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();

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
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();

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
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();

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
            Exception exception = Record.Exception(() => administrationService.TagUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void TagUser_Throws_ValidationException_If_Given_UserId_Is_Less_Than_1()
        {
            // Arrange
            UserTaggingRequest request = new UserTaggingRequest { UserId = 0, TagId = 2 };
            // Act
            Exception exception = Record.Exception(() => administrationService.TagUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("UserId"));
        }

        [Fact]
        public void TagUser_Throws_ValidationException_If_Given_TagId_Is_Less_Than_1()
        {
            // Arrange
            UserTaggingRequest request = new UserTaggingRequest { UserId = 2, TagId = 0 };
            // Act
            Exception exception = Record.Exception(() => administrationService.TagUser(request));
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
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();

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
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();

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
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();

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
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();

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
            Exception exception = Record.Exception(() => administrationService.RemoveDoorAccess(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void RemoveDoorAccess_Throws_ValidationException_If_Given_TagId_Is_Less_Than_1()
        {
            // Arrange
            DoorAccessRemovalRequest request = new() { TagId = 0, DoorId = 2 };
            // Act
            Exception exception = Record.Exception(() => administrationService.RemoveDoorAccess(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("TagId"));
        }

        [Fact]
        public void RemoveDoorAccess_Throws_ValidationException_If_Given_DoorId_Is_Less_Than_1()
        {
            // Arrange
            DoorAccessRemovalRequest request = new() { DoorId = 0, TagId = 2 };
            // Act
            Exception exception = Record.Exception(() => administrationService.RemoveDoorAccess(request));
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
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();

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
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();

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
            Exception exception = Record.Exception(() => administrationService.RemoveUserTag(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void RemoveUserTag_Throws_ValidationException_If_Given_TagId_Is_Less_Than_1()
        {
            // Arrange
            UserTagRemovalRequest request = new() { TagId = 0, UserId = 2 };
            // Act
            Exception exception = Record.Exception(() => administrationService.RemoveUserTag(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("TagId"));
        }

        [Fact]
        public void RemoveUserTag_Throws_ValidationException_If_Given_UserId_Is_Less_Than_1()
        {
            // Arrange
            UserTagRemovalRequest request = new() { TagId = 2, UserId = 0 };
            // Act
            Exception exception = Record.Exception(() => administrationService.RemoveUserTag(request));
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
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();

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
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();

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
            Exception exception = Record.Exception(() => administrationService.DeleteUser(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void DeleteUser_Throws_ValidationException_If_Given_UserId_Is_Less_Than_1()
        {
            // Arrange
            UserDeletionRequest request = new() { UserId = 0 };
            // Act
            Exception exception = Record.Exception(() => administrationService.DeleteUser(request));
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
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();

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
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();

            UserDeletionRequest request = new() { UserId = 2 };
            // Act
            administrationServiceToSetup.DeleteUser(request);
            // Assert
            mockUnitOfWork.Verify(muw => muw.UserRepository.Delete(2), Times.Once);
        }

        [Fact]
        public void DeleteDoor_Throws_ValidationException_If_Given_Request_Is_Null()
        {
            // Arrange
            DoorDeletionRequest request = null;
            // Act
            Exception exception = Record.Exception(() => administrationService.DeleteDoor(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void DeleteDoor_Throws_ValidationException_If_Given_DoorId_Is_Less_Than_1()
        {
            // Arrange
            DoorDeletionRequest request = new() { DoorId = 0 };
            // Act
            Exception exception = Record.Exception(() => administrationService.DeleteDoor(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("DoorId"));
        }

        [Fact]
        public void DeleteDoor_Throws_ValidationException_If_Door_Already_Has_Not_Tag_In_Given_Parameters()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.DoorRepository.CheckIfDoorAlreadyExists(2))
                .Returns(false);

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();

            DoorDeletionRequest request = new() { DoorId = 2 };
            // Act
            Exception exception = Record.Exception(() => administrationServiceToSetup.DeleteDoor(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("There is no such a door already!"));
        }

        [Fact]
        public void DeleteDoor_Deletes_User_Entity_From_DoorRepository_By_Given_Parameters_If_Request_Is_Valid()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.DoorRepository.CheckIfDoorAlreadyExists(2))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.DoorRepository.Delete(2));
            mockUnitOfWork.Setup(muw => muw.SaveChanges());

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();

            DoorDeletionRequest request = new() { DoorId = 2 };
            // Act
            administrationServiceToSetup.DeleteDoor(request);
            // Assert
            mockUnitOfWork.Verify(muw => muw.DoorRepository.Delete(2), Times.Once);
        }

        [Fact]
        public void DeleteTag_Throws_ValidationException_If_Given_Request_Is_Null()
        {
            // Arrange
            TagDeletionRequest request = null;
            // Act
            Exception exception = Record.Exception(() => administrationService.DeleteTag(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("Request cannot be null!"));
        }

        [Fact]
        public void DeleteTag_Throws_ValidationException_If_Given_TagId_Is_Less_Than_1()
        {
            // Arrange
            TagDeletionRequest request = new() { TagId = 0 };
            // Act
            Exception exception = Record.Exception(() => administrationService.DeleteTag(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("TagId"));
        }

        [Fact]
        public void DeleteTag_Throws_ValidationException_If_Tag_Already_Has_Not_Tag_In_Given_Parameters()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.TagRepository.CheckIfTagAlreadyExists(2))
                .Returns(false);

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();

            TagDeletionRequest request = new() { TagId = 2 };
            // Act
            Exception exception = Record.Exception(() => administrationServiceToSetup.DeleteTag(request));
            // Assert
            Assert.True(exception is ValidationException && exception.Message.Contains("There is no such a tag already!"));
        }

        [Fact]
        public void DeleteTag_Deletes_User_Entity_From_TagRepository_By_Given_Parameters_If_Request_Is_Valid()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.TagRepository.CheckIfTagAlreadyExists(2))
                .Returns(true);
            mockUnitOfWork.Setup(muw => muw.TagRepository.Delete(2));
            mockUnitOfWork.Setup(muw => muw.SaveChanges());

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();

            TagDeletionRequest request = new() { TagId = 2 };
            // Act
            administrationServiceToSetup.DeleteTag(request);
            // Assert
            mockUnitOfWork.Verify(muw => muw.TagRepository.Delete(2), Times.Once);
        }

        [Fact]
        public void GetAllDoors_Receives_All_Doors_Entities_From_DoorRepository()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.DoorRepository.GetList(It.IsAny<Expression<Func<Data.Entities.Door, bool>>>()))
                .Returns(new List<Data.Entities.Door> { new Data.Entities.Door { Id = 1234 } });

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();
            // Act
            var actualResult = administrationServiceToSetup.GetAllDoors();
            // Assert
            Assert.Contains(actualResult, door => door.Id == 1234);
        }

        [Fact]
        public void GetAllTags_Calls_GetAllTags_From_TagRepository_To_Get_All_Tags()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new();
            mockUnitOfWork.Setup(muw => muw.TagRepository.GetAllTags())
                .Returns(new List<Data.Entities.Tag> { new Data.Entities.Tag { Id = 1234 } });

            TestBusinessModuleInitializer testModule = new(mockUnitOfWork.Object, (new Mock<IEncryptionUtilities>()).Object);
            IAdministrationService administrationServiceToSetup = testModule.GetService<IAdministrationService>();
            // Act
            var actualResult = administrationServiceToSetup.GetAllTags();
            // Assert
            Assert.Contains(actualResult, tag => tag.Id == 1234);
        }
    }
}
