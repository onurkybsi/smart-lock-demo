using FluentValidation;
using SmartLockDemo.Business.Service.User;
using SmartLockDemo.Business.Utilities;
using SmartLockDemo.Data;
using SmartLockDemo.Infrastructure.Utilities;

namespace SmartLockDemo.Business.Service.SmartLockAdministration
{
    internal class SmartLockAdministrationService : ISmartLockAdministrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IValidatorAccessor _validatorAccessor;

        public SmartLockAdministrationService(IUnitOfWork unitOfWork, IValidatorAccessor validatorAccessor,
            IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _validatorAccessor = validatorAccessor;
            _userService = userService;
        }

        public UserCreationResult CreateUser(UserCreationRequest request)
            => _userService.CreateUser(request);

        public DoorCreationResult CreateDoor(DoorCreationRequest request)
        {
            if (request is null)
                throw new ValidationException("Request cannot be null!");
            _validatorAccessor.DoorCreationRequest.ValidateWithExceptionOption(request);

            _unitOfWork.DoorRepository.Add(new Data.Entities.Door { Name = request.Name });
            _unitOfWork.SaveChanges();
            return new DoorCreationResult(true);
        }
    }
}
