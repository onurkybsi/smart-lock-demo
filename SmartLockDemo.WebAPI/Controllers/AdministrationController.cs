using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLockDemo.Business.Service.Administration;
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
        private readonly IAdministrationService _administrationService;
        private readonly IUserService _userService;

        public AdministrationController(IAdministrationService administrationService, IUserService userService)
        {
            _administrationService = administrationService;
            _userService = userService;
        }

        /// <summary>
        /// Creates a new user in the system
        /// </summary>
        /// <param name="request">User creation parameters</param>
        /// <returns>Result of creation operation</returns>
        [HttpPost(RestServiceUris.Administration.CreateUser)]
        public IActionResult CreateUser([FromBody] UserCreationRequest request)
            => Created(RestServiceUris.Administration.CreateUser, _administrationService.CreateUser(request));

        /// <summary>
        /// Creates a new door in the system
        /// </summary>
        /// <param name="request">Door creation parameters</param>
        /// <returns>Result of creation operation</returns>
        [HttpPost(RestServiceUris.Administration.CreateDoor)]
        public IActionResult CreateDoor([FromBody] DoorCreationRequest request)
            => Created(RestServiceUris.Administration.CreateDoor, _administrationService.CreateDoor(request));

        /// <summary>
        /// Creates a new tag in the system
        /// </summary>
        /// <param name="request">Tag creation parameters</param>
        /// <returns>Result of creation operation</returns>
        [HttpPost(RestServiceUris.Administration.CreateTag)]
        public IActionResult CreateTag([FromBody] TagCreationRequest request)
            => Created(RestServiceUris.Administration.CreateTag, _administrationService.CreateTag(request));

        /// <summary>
        /// Creates a new access to a door
        /// </summary>
        /// <param name="request">Door access creation parameters</param>
        /// <returns>Result of creation operation</returns>
        [HttpPost(RestServiceUris.Administration.CreateDoorAccess)]
        public IActionResult CreateDoorAccess([FromBody] DoorAccessCreationRequest request)
            => Created(RestServiceUris.Administration.CreateDoorAccess,
                _administrationService.CreateDoorAccess(request));

        /// <summary>
        /// Tags an user to extend its door access as accessible doors under this tag
        /// </summary>
        /// <param name="request">User tagging parameters</param>
        /// <returns></returns>
        [HttpPost(RestServiceUris.Administration.TagUser)]
        public IActionResult TagUser([FromBody] UserTaggingRequest request)
            => Created(RestServiceUris.Administration.TagUser,
                _administrationService.TagUser(request));

        /// <summary>
        /// Removes a door access from a tag
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="doorId"></param>
        /// <returns></returns>
        [HttpDelete(RestServiceUris.Administration.RemoveDoorAccess)]
        public IActionResult RemoveDoorAccess([FromQuery] int tagId, int doorId)
            => Ok(_administrationService.RemoveDoorAccess(new DoorAccessRemovalRequest
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
            => Ok(_administrationService.RemoveUserTag(new UserTagRemovalRequest { UserId = userId, TagId = tagId }));

        /// <summary>
        /// Deletes an user from the system
        /// </summary>
        /// <param name="userId">User id to delete</param>
        /// <returns></returns>
        [HttpDelete(RestServiceUris.Administration.DeleteUser)]
        public IActionResult DeleteUser([FromQuery] int userId)
        {
            if (_userService.CheckIfUserIsAdmin(userId))
                return StatusCode(StatusCodes.Status403Forbidden);
            return Ok(_administrationService.DeleteUser(new UserDeletionRequest { UserId = userId }));
        }

        /// <summary>
        /// Deletes a door from the system
        /// </summary>
        /// <param name="doorId">Door id to delete</param>
        /// <returns></returns>
        [HttpDelete(RestServiceUris.Administration.DeleteDoor)]
        public IActionResult DeleteDoor([FromQuery] int doorId)
            => Ok(_administrationService.DeleteDoor(new DoorDeletionRequest { DoorId = doorId }));

        /// <summary>
        /// Deletes a tag from the system
        /// </summary>
        /// <param name="tagId">Tag id to delete</param>
        /// <returns></returns>
        [HttpDelete(RestServiceUris.Administration.DeleteTag)]
        public IActionResult DeleteTag([FromQuery] int tagId)
            => Ok(_administrationService.DeleteTag(new TagDeletionRequest { TagId = tagId }));
    }
}
