﻿using SmartLockDemo.Business.Service.User;
using System.Collections.Generic;

namespace SmartLockDemo.Business.Service.Administration
{
    /// <summary>
    /// Provides system administration functionalities for admins
    /// </summary>
    public interface IAdministrationService
    {
        /// <summary>
        /// Adds a new user to the system
        /// </summary>
        /// <param name="request">User creation parameters</param>
        /// <returns></returns>
        UserCreationResult CreateUser(UserCreationRequest request);

        /// <summary>
        /// Adds a new door to the system
        /// </summary>
        /// <param name="request">Door creation parameters</param>
        /// <returns></returns>
        DoorCreationResult CreateDoor(DoorCreationRequest request);

        /// <summary>
        /// Adds a new tag to the system
        /// </summary>
        /// <param name="request">Tag creation parameters</param>
        /// <returns></returns>
        TagCreationResult CreateTag(TagCreationRequest request);

        /// <summary>
        /// Sets a tag so that a door can be accessed
        /// </summary>
        /// <param name="request">Access creation parameters</param>
        /// <returns></returns>
        DoorAccessCreationResult CreateDoorAccess(DoorAccessCreationRequest request);

        /// <summary>
        /// Tags a user to access doors under that tag
        /// </summary>
        /// <param name="request">Tagging parameters</param>
        /// <returns></returns>
        UserTaggingResult TagUser(UserTaggingRequest request);

        /// <summary>
        /// Removes the door access from a tag
        /// </summary>
        /// <param name="request">Removal operation parameters</param>
        /// <returns></returns>
        DoorAccessRemovalResult RemoveDoorAccess(DoorAccessRemovalRequest request);

        /// <summary>
        /// Removes a tag from an user
        /// </summary>
        /// <param name="request">User tag removal parameters</param>
        /// <returns></returns>
        UserTagRemovalResult RemoveUserTag(UserTagRemovalRequest request);

        /// <summary>
        /// Deletes an user from the system
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        UserDeletionResult DeleteUser(UserDeletionRequest request);

        /// <summary>
        /// Deletes a door from the system
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        DoorDeletionResult DeleteDoor(DoorDeletionRequest request);

        /// <summary>
        /// Deletes a tag from the system
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        TagDeletionResult DeleteTag(TagDeletionRequest request);

        /// <summary>
        /// Receives all users from the UserRepository
        /// </summary>
        /// <returns></returns>
        List<User.User> GetAllUsers();

        /// <summary>
        /// Receives all doors from the DoorRepository
        /// </summary>
        /// <returns></returns>
        List<Door> GetAllDoors();

        /// <summary>
        /// Receives all tags from the TagRepository
        /// </summary>
        /// <returns></returns>
        List<Tag> GetAllTags();
    }
}
