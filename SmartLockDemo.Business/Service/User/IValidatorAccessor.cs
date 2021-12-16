namespace SmartLockDemo.Business.Service.User
{
    internal interface IValidatorAccessor
    {
        public UserCreationRequestValidator UserCreationRequest { get; }
    }
}
