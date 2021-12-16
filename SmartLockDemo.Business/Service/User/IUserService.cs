namespace SmartLockDemo.Business.Service.User
{
    public interface IUserService
    {
        DoorAccessControlResult CheckDoorAccess(DoorAccessContext context);
    }
}
