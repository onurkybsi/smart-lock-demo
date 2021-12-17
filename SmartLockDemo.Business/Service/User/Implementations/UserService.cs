using FluentValidation;
using SmartLockDemo.Business.Utilities;
using SmartLockDemo.Infrastructure.Utilities;
using System;
using System.Linq;

namespace SmartLockDemo.Business.Service.User
{
    /// <summary>
    /// Default implementation of IUserService service
    /// </summary>
    internal class UserService : IUserService
    {
        private readonly Data.IUnitOfWork unitOfWork;
        private readonly IValidatorAccessor validatorAccessor;
        private readonly IEncryptionUtilities encryptionUtilities;

        public UserService(Data.IUnitOfWork unitOfWork, IValidatorAccessor validatorAccessor, IEncryptionUtilities encryptionUtilities)
        {
            this.unitOfWork = unitOfWork;
            this.validatorAccessor = validatorAccessor;
            this.encryptionUtilities = encryptionUtilities;
        }

        public UserCreationResult CreateUser(UserCreationRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));
            validatorAccessor.UserCreationRequest.ValidateWithExceptionOption(request);

            unitOfWork.UserRepository.Add(new Data.Entites.User
            {
                Email = request.Email,
                HashedPassord = encryptionUtilities.Hash(request.Password),
                Role = (byte)Role.User
            });
            unitOfWork.SaveChanges();

            return new UserCreationResult(true);
        }

        public DoorAccessControlResult CheckDoorAccess(DoorAccessControlRequest request)
        {
            validateDoorAccessControlRequest(request);
            bool userHasTheTag = (from td in unitOfWork.TagDoorRepository.GetTable()
                                  join ut in unitOfWork.UserTagRepository.GetTable() on td.TagId equals ut.TagId
                                  where td.DoorId == request.DoorId && ut.UserId == request.UserId
                                  select new { }).Any();
            return new DoorAccessControlResult { IsUserAuthorized = userHasTheTag };
        }

        private static void validateDoorAccessControlRequest(DoorAccessControlRequest context) { }
    }
}
