using SmartLockDemo.Business.Service.User;

namespace SmartLockDemo.Business.Utilities
{
    internal class ValidatorAccessor : IValidatorAccessor
    {
        private static readonly UserCreationRequestValidator userCreationRequest = new();
        public UserCreationRequestValidator UserCreationRequest { get => userCreationRequest; }
    }
}
