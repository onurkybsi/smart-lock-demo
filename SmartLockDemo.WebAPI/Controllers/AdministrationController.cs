using Microsoft.AspNetCore.Mvc;
using SmartLockDemo.Business.Service.SmartLockAdministration;
using SmartLockDemo.Business.Service.User;

namespace SmartLockDemo.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AdministrationController : ControllerBase
    {
        private readonly ISmartLockAdministrationService smartLockAdministrationService;

        public AdministrationController(ISmartLockAdministrationService smartLockAdministrationService)
        {
            this.smartLockAdministrationService = smartLockAdministrationService;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserCreationRequest request)
            => Ok(smartLockAdministrationService.CreateUser(request));
    }
}
