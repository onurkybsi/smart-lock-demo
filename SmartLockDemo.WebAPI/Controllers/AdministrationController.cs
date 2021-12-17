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
    }
}
