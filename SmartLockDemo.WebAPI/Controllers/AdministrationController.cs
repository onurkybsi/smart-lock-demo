﻿using Microsoft.AspNetCore.Mvc;
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
    }
}
