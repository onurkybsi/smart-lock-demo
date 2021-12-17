using SmartLockDemo.Business.Service.SmartLockAdministration;
using SmartLockDemo.Business.Service.User;
using SmartLockDemo.Data;

namespace SmartLockDemo.Business.Utilities
{
    internal class ValidatorAccessor : IValidatorAccessor
    {
        private readonly UserCreationRequestValidator _userCreationRequestValidator;
        private readonly DoorAccessControlRequestValidator _doorAccessControlRequestValidator;
        private readonly DoorCreationRequestValidator _doorCreationRequestValidator;
        private readonly TagCreationRequestValidator _tagCreationRequestValidator;

        public ValidatorAccessor(IUnitOfWork unitOfWork)
        {
            _userCreationRequestValidator = new UserCreationRequestValidator(unitOfWork);
            _doorAccessControlRequestValidator = new DoorAccessControlRequestValidator();
            _doorCreationRequestValidator = new DoorCreationRequestValidator(unitOfWork);
            _tagCreationRequestValidator = new TagCreationRequestValidator(unitOfWork);
        }

        public UserCreationRequestValidator UserCreationRequest { get => _userCreationRequestValidator; }
        public DoorAccessControlRequestValidator DoorAccessControlRequest { get => _doorAccessControlRequestValidator; }
        public DoorCreationRequestValidator DoorCreationRequest { get => _doorCreationRequestValidator; }
        public TagCreationRequestValidator TagCreationRequest { get => _tagCreationRequestValidator; }
    }
}
