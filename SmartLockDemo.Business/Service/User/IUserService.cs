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

        /// <summary>
        /// Updates user by given values
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        UserUpdateResult UpdateUser(UserUpdateRequest request);

        /// <summary>
        /// Logs the user into the system
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        LogInResult LogIn(LogInRequest request);

        /// <summary>
        /// Checks if user is admin or not
        /// </summary>
        /// <param name="userIdToCheck">Id of user to check</param>
        /// <returns>True if user is admin</returns>
        bool CheckIfUserIsAdmin(int userIdToCheck);
    }
}
