using FluentValidation;
using SmartLockDemo.Business.Service.User;
using SmartLockDemo.Business.Utilities;
using SmartLockDemo.Data;
using SmartLockDemo.Infrastructure.Utilities;

namespace SmartLockDemo.Business.Service.Administration
{
    internal class AdministrationService : IAdministrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IValidatorAccessor _validatorAccessor;

        public AdministrationService(IUnitOfWork unitOfWork, IValidatorAccessor validatorAccessor,
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

        public TagCreationResult CreateTag(TagCreationRequest request)
        {
            if (request is null)
                throw new ValidationException("Request cannot be null!");
            _validatorAccessor.TagCreationRequest.ValidateWithExceptionOption(request);

            _unitOfWork.TagRepository.Add(new Data.Entities.Tag { Name = request.Name });
            _unitOfWork.SaveChanges();

            return new TagCreationResult(true);
        }

        public DoorAccessCreationResult CreateDoorAccess(DoorAccessCreationRequest request)
        {
            if (request is null)
                throw new ValidationException("Request cannot be null!");
            _validatorAccessor.DoorAccessCreationRequest.ValidateWithExceptionOption(request);

            _unitOfWork.TagDoorRepository.Add(new Data.Entities.TagDoor
            {
                DoorId = request.DoorId,
                TagId = request.TagId
            });
            _unitOfWork.SaveChanges();

            return new DoorAccessCreationResult(true);
        }

        public UserTaggingResult TagUser(UserTaggingRequest request)
        {
            if (request is null)
                throw new ValidationException("Request cannot be null!");
            _validatorAccessor.UserTaggingRequest.ValidateWithExceptionOption(request);

            _unitOfWork.UserTagRepository.Add(new Data.Entities.UserTag
            {
                UserId = request.UserId,
                TagId = request.TagId
            });
            _unitOfWork.SaveChanges();

            return new UserTaggingResult(true);
        }

        public DoorAccessRemovalResult RemoveDoorAccess(DoorAccessRemovalRequest request)
        {
            if (request is null)
                throw new ValidationException("Request cannot be null!");
            _validatorAccessor.DoorAccessRemovalRequest.ValidateWithExceptionOption(request);

            _unitOfWork.TagDoorRepository.Remove(request.TagId, request.DoorId);
            _unitOfWork.SaveChanges();

            return new DoorAccessRemovalResult(true);
        }

        public UserTagRemovalResult RemoveUserTag(UserTagRemovalRequest request)
        {
            if (request is null)
                throw new ValidationException("Request cannot be null!");
            _validatorAccessor.UserTagRemovalRequest.ValidateWithExceptionOption(request);

            _unitOfWork.UserTagRepository.Remove(request.UserId, request.TagId);
            _unitOfWork.SaveChanges();

            return new UserTagRemovalResult(true);
        }

        public UserDeletionResult DeleteUser(UserDeletionRequest request)
        {
            if (request is null)
                throw new ValidationException("Request cannot be null!");
            _validatorAccessor.UserDeletionRequest.ValidateWithExceptionOption(request);

            _unitOfWork.UserRepository.Delete(request.UserId);
            _unitOfWork.SaveChanges();

            return new UserDeletionResult(true);
        }
    }
}
