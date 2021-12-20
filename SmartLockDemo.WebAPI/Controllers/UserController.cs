using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLockDemo.Business.Service.User;
using SmartLockDemo.WebAPI.Utilities;

namespace SmartLockDemo.WebAPI.Controllers
{
    [ApiController]
    [Authorize]
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
        public IActionResult UpdateUser([FromBody] UserUpdateBaseRequest request)
            => Ok(_userService.UpdateUser(new UserUpdateRequest(request) { Id = HttpContext.GetUserId() }));

        /// <summary>
        /// Logs the user into the system
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost(RestServiceUris.User.Login)]
        public IActionResult Login([FromBody] LogInRequest request)
        {
            LogInResult result = _userService.LogIn(request);
            return result.IsSuccessful
                ? Ok(result)
                : BadRequest(result);
        }
    }
}
