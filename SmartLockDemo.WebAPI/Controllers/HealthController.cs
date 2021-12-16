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
        private readonly Data.IUnitOfWork _unitOfWork;

        public HealthController(Data.IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

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
        public IActionResult CheckSomething()
            => Ok(_unitOfWork.UserRepository.GetList(user => true));
    }
}