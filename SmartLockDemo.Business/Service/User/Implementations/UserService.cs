using FluentValidation;
using SmartLockDemo.Business.Utilities;
using SmartLockDemo.Infrastructure.Utilities;
using System.Linq;

namespace SmartLockDemo.Business.Service.User
{
    /// <summary>
    /// Default implementation of IUserService service
    /// </summary>
    internal class UserService : IUserService
    {
        private readonly Data.IUnitOfWork _unitOfWork;
        private readonly IValidatorAccessor _validatorAccessor;
        private readonly IEncryptionUtilities _encryptionUtilities;

        public UserService(Data.IUnitOfWork unitOfWork, IValidatorAccessor validatorAccessor, IEncryptionUtilities encryptionUtilities)
        {
            _unitOfWork = unitOfWork;
            _validatorAccessor = validatorAccessor;
            _encryptionUtilities = encryptionUtilities;
        }

        public UserCreationResult CreateUser(UserCreationRequest request)
        {
            if (request is null)
                throw new ValidationException("Request cannot be null!");
            _validatorAccessor.UserCreationRequest.ValidateWithExceptionOption(request);

            _unitOfWork.UserRepository.Add(new Data.Entities.User
            {
                Email = request.Email,
                HashedPassword = _encryptionUtilities.Hash(request.Password),
                Role = (byte)Role.User
            });
            _unitOfWork.SaveChanges();

            return new UserCreationResult(true);
        }

        public DoorAccessControlResult CheckDoorAccess(DoorAccessControlRequest request)
        {
            if (request is null)
                throw new ValidationException("Request cannot be null!");
            _validatorAccessor.DoorAccessControlRequest.ValidateWithExceptionOption(request);

            bool userHasTheTag = (from td in _unitOfWork.TagDoorRepository.GetTable()
                                  join ut in _unitOfWork.UserTagRepository.GetTable() on td.TagId equals ut.TagId
                                  where td.DoorId == request.DoorId && ut.UserId == request.UserId
                                  select new { }).Any();
            return new DoorAccessControlResult { IsUserAuthorized = userHasTheTag };
        }
    }
}
