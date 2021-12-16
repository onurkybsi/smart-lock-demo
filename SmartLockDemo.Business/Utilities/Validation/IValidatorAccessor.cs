using SmartLockDemo.Business.Service.User;

namespace SmartLockDemo.Business.Utilities
{
    internal interface IValidatorAccessor
    {
        public UserCreationRequestValidator UserCreationRequest { get; }
    }
}
