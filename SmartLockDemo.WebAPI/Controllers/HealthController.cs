using Microsoft.AspNetCore.Mvc;

namespace SmartLockDemo.WebAPI.Controllers
{
    /// <summary>
    /// Contains an endpoint which gives liveness information of the server
    /// </summary>
    [ApiController]
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