namespace SmartLockDemo.Business.Service.User
{
    /// <summary>
    /// Implement business rules of User domain
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Creates a new user in the smart-lock access system
        /// </summary>
        /// <param name="request">Creation parameters of an user</param>
        /// <returns>Result of creation operation</returns>
        UserCreationResult CreateUser(UserCreationRequest request);

        /// <summary>
        /// Checks given user has access to given door
        /// </summary>
        /// <param name="request">Parameters for door access control</param>
        /// <returns>Door access control result</returns>
        DoorAccessControlResult CheckDoorAccess(DoorAccessControlRequest request);
    }
}
