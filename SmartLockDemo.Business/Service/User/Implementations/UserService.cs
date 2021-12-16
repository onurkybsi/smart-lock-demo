using System.Linq;

namespace SmartLockDemo.Business.Service.User
{
    internal class UserService : IUserService
    {
        private readonly Data.IUnitOfWork unitOfWork;

        public UserService(Data.IUnitOfWork unitOfWork)
            => this.unitOfWork = unitOfWork;

        public DoorAccessControlResult CheckDoorAccess(DoorAccessContext context)
        {
            validateDoorAccessContext(context);
            bool userHasTheTag = (from td in unitOfWork.TagDoorRepository.GetTable()
                                  join ut in unitOfWork.UserTagRepository.GetTable() on td.TagId equals ut.TagId
                                  where td.DoorId == context.DoorId && ut.UserId == context.UserId
                                  select new { }).Any();
            return new DoorAccessControlResult { IsUserAuthorized = userHasTheTag };
        }

        private void validateDoorAccessContext(DoorAccessContext context) { }
    }
}
