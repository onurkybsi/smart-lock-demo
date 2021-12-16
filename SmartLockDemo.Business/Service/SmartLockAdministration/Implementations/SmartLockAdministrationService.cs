using SmartLockDemo.Business.Service.User;

namespace SmartLockDemo.Business.Service.SmartLockAdministration
{
    internal class SmartLockAdministrationService : ISmartLockAdministrationService
    {
        private readonly IUserService userService;

        public SmartLockAdministrationService(IUserService userService)
        {
            this.userService = userService;
        }

        public UserCreationResult CreateUser(UserCreationRequest request)
            => userService.CreateUser(request);
    }
}
