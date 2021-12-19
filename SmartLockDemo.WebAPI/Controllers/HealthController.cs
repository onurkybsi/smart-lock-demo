using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartLockDemo.WebAPI.Controllers
{
    /// <summary>
    /// Provides a REST service which gives liveness information of the server
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("[controller]/[action]")]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Returns 200 if the server is running
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Check()
            => Ok("I'm healthy!");
    }
}