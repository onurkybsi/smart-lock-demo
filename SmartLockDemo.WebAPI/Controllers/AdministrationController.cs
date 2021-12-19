using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartLockDemo.Business.Service.SmartLockAdministration;
using SmartLockDemo.Business.Service.User;
using SmartLockDemo.WebAPI.Utilities;

namespace SmartLockDemo.WebAPI.Controllers
{
    /// <summary>
    /// Provides REST services to administrate the system
    /// </summary>
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdministrationController : ControllerBase
    {
        private readonly IAdministrationService _smartLockAdministrationService;

        public AdministrationController(IAdministrationService smartLockAdministrationService)
            => _smartLockAdministrationService = smartLockAdministrationService;

        /// <summary>
        /// Creates a new user in the system
        /// </summary>
        /// <param name="request">User creation parameters</param>
        /// <returns>Result of creation operation</returns>
        [HttpPost(RestServiceUris.Administration.CreateUser)]
        public IActionResult CreateUser([FromBody] UserCreationRequest request)
            => Created(RestServiceUris.Administration.CreateUser, _smartLockAdministrationService.CreateUser(request));

        /// <summary>
        /// Creates a new door in the system
        /// </summary>
        /// <param name="request">Door creation parameters</param>
        /// <returns>Result of creation operation</returns>
        [HttpPost(RestServiceUris.Administration.CreateDoor)]
        public IActionResult CreateDoor([FromBody] DoorCreationRequest request)
            => Created(RestServiceUris.Administration.CreateDoor, _smartLockAdministrationService.CreateDoor(request));

        /// <summary>
        /// Creates a new tag in the system
        /// </summary>
        /// <param name="request">Tag creation parameters</param>
        /// <returns>Result of creation operation</returns>
        [HttpPost(RestServiceUris.Administration.CreateTag)]
        public IActionResult CreateTag([FromBody] TagCreationRequest request)
            => Created(RestServiceUris.Administration.CreateTag, _smartLockAdministrationService.CreateTag(request));

        /// <summary>
        /// Creates a new access to a door
        /// </summary>
        /// <param name="request">Door access creation parameters</param>
        /// <returns>Result of creation operation</returns>
        [HttpPost(RestServiceUris.Administration.CreateDoorAccess)]
        public IActionResult CreateDoorAccess([FromBody] DoorAccessCreationRequest request)
            => Created(RestServiceUris.Administration.CreateDoorAccess,
                _smartLockAdministrationService.CreateDoorAccess(request));

        /// <summary>
        /// Tags an user to extend its door access as accessible doors under this tag
        /// </summary>
        /// <param name="request">User tagging parameters</param>
        /// <returns></returns>
        [HttpPost(RestServiceUris.Administration.TagUser)]
        public IActionResult TagUser([FromBody] UserTaggingRequest request)
            => Created(RestServiceUris.Administration.TagUser,
                _smartLockAdministrationService.TagUser(request));

        /// <summary>
        /// Removes a door access from a tag
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="doorId"></param>
        /// <returns></returns>
        [HttpDelete(RestServiceUris.Administration.RemoveDoorAccess)]
        public IActionResult RemoveDoorAccess([FromQuery] int tagId, int doorId)
            => Ok(_smartLockAdministrationService.RemoveDoorAccess(new DoorAccessRemovalRequest
            {
                TagId = tagId,
                DoorId = doorId
            }));

        /// <summary>
        /// Removes the tag from the user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tagId"></param>
        /// <returns></returns>
        [HttpDelete(RestServiceUris.Administration.RemoveUserTag)]
        public IActionResult RemoveUserTag([FromQuery] int userId, [FromQuery] int tagId)
            => Ok(_smartLockAdministrationService.RemoveUserTag(new UserTagRemovalRequest { UserId = userId, TagId = tagId }));

        /// <summary>
        /// Deletes an user from the system
        /// </summary>
        /// <param name="userId">User id to delete</param>
        /// <returns></returns>
        [HttpDelete(RestServiceUris.Administration.DeleteUser)]
        public IActionResult DeleteUser([FromQuery] int userId)
            => Ok(_smartLockAdministrationService.DeleteUser(new UserDeletionRequest { UserId = userId }));

    }
}
