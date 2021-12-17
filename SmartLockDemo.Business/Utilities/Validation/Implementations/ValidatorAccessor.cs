using SmartLockDemo.Business.Service.User;
using SmartLockDemo.Data;

namespace SmartLockDemo.Business.Utilities
{
    internal class ValidatorAccessor : IValidatorAccessor
    {
        private readonly UserCreationRequestValidator userCreationRequest;

        public ValidatorAccessor(IUnitOfWork unitOfWork)
        {
            userCreationRequest = new UserCreationRequestValidator(unitOfWork);
        }

        public UserCreationRequestValidator UserCreationRequest { get => userCreationRequest; }
    }
}
