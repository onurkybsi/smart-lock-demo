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
    public class AdministrationController : ControllerBase
    {
        private readonly ISmartLockAdministrationService _smartLockAdministrationService;

        public AdministrationController(ISmartLockAdministrationService smartLockAdministrationService)
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

        // RemoveDoorAccess
        [HttpDelete(RestServiceUris.Administration.RemoveDoorAccess)]
        public IActionResult RemoveDoorAccess([FromQuery] int tagId, int doorId)
            => Ok(_smartLockAdministrationService.RemoveDoorAccess(new DoorAccessRemovalRequest
            {
                TagId = tagId,
                DoorId = doorId
            }));

        // RemoveTagFromUser
        // DeleteUser
    }
}
