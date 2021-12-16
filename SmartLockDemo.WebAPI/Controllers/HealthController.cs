using Microsoft.AspNetCore.Mvc;
using SmartLockDemo.Business.Service.User;

namespace SmartLockDemo.WebAPI.Controllers
{
    /// <summary>
    /// Contains an endpoint which gives liveness information of the server
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class HealthController : ControllerBase
    {
        private readonly IUserService _userService;

        public HealthController(IUserService userService)
            => _userService = userService;

        /// <summary>
        /// Returns 200 if the server is running
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Check()
            => Ok("I'm healthy!");

        /// <summary>
        /// REST service to check something in API
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CheckSomething([FromQuery] int doorId, [FromQuery] int userId)
            => Ok(_userService.CheckDoorAccess(new DoorAccessContext { DoorId = doorId, UserId = userId }));
    }
}