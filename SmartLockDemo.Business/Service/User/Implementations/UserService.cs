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

        public UserService(Data.IUnitOfWork unitOfWork, IValidatorAccessor validatorAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.validatorAccessor = validatorAccessor;
        }

        public UserCreationResult CreateUser(UserCreationRequest request)
        {
            return new UserCreationResult(validatorAccessor.UserCreationRequest.Validate(request));
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
