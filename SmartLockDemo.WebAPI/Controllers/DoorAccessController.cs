using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartLockDemo.Business.Service.User;
using SmartLockDemo.WebAPI.Utilities;

namespace SmartLockDemo.WebAPI.Controllers
{
    /// <summary>
    /// Provides REST services to administrate door access
    /// </summary>
    [ApiController]
    [Authorize]
    public class DoorAccessController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<DoorAccessController> _logger;

        public DoorAccessController(IUserService userService, ILogger<DoorAccessController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Checks if the user whose ID is sent has access to the door whose ID is sent. 
        /// </summary>
        /// <param name="userId">Id of the user that wants to access the door</param>
        /// <param name="doorId">Id of the door that is wanted to access</param>
        /// <returns>Access control result</returns>
        [HttpGet(RestServiceUris.DoorAccess.CheckDoorAccess)]
        public IActionResult CheckDoorAccess([FromRoute] int id)
        {
            DoorAccessControlResult result = _userService.CheckDoorAccess(new DoorAccessControlRequest
            {
                UserId = HttpContext.GetUserId(),
                DoorId = id
            });

            if (result.IsUserAuthorized)
            {
                _logger.LogInformation($"User: {HttpContext.GetUserId()} accessed to Door: {id}");
                return Ok();
            }
            else
            {
                _logger.LogInformation($"User: {HttpContext.GetUserId()} failed to access Door: {id}");
                return StatusCode(StatusCodes.Status403Forbidden);
            }
        }
    }
}
