namespace SmartLockDemo.Data.Repositories
{
    /// <summary>
    /// Provides some functionalities to manage user's door access
    /// </summary>
    public interface IUserDoorAccessRepository
    {
        /// <summary>
        /// Checks that user can access the door by doorId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="doorId"></param>
        /// <returns></returns>
        bool CheckThatUserHasAccessTheDoor(int userId, int doorId);

        /// <summary>
        /// Tries to set user doors accessibility status by the given tagId and canUserAccessDoorsOfThisTag in cache
        /// If the cache is not active or not available does nothing
        /// </summary>
        /// <param name="userId">User ID will be set</param>
        /// <param name="tagId">Tag ID which is removed from user</param>
        /// <param name="canUserAccessDoorsOfThisTag">The user by given ID will access the doors of the tag or not</param>
        void TryToSetAccessibilityOfUserInCache(int userId, int tagId, bool canUserAccessDoorsOfThisTag);
    }
}
