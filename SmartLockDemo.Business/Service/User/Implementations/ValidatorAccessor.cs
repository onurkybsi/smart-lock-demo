namespace SmartLockDemo.Business.Service.User
{
    internal class ValidatorAccessor : IValidatorAccessor
    {
        private static UserCreationRequestValidator userCreationRequest = new UserCreationRequestValidator();
        public UserCreationRequestValidator UserCreationRequest { get => userCreationRequest; }
    }
}
