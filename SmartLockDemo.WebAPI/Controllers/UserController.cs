using Microsoft.AspNetCore.Mvc;
using SmartLockDemo.Business.Service.User;
using SmartLockDemo.WebAPI.Utilities;

namespace SmartLockDemo.WebAPI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
            => _userService = userService;

        /// <summary>
        /// Updates user by given values
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut(RestServiceUris.User.UpdateUser)]
        public IActionResult UpdateUser([FromBody] UserUpdateRequest request)
            => Ok(_userService.UpdateUser(request));

        // TO-DO: SignIn
    }
}
