using KybInfrastructure.Data;
using SmartLockDemo.Data.Entities;

namespace SmartLockDemo.Data.Repositories
{
    /// <summary>
    /// Contains various utilities for User database entity operations
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        bool CheckIfEmailAlreadyExists(string emailToCheck);

        /// <summary>
        /// Checks the user is exist in the system or not
        /// </summary>
        /// <param name="userId">Id of user will be checked</param>
        /// <returns></returns>
        bool CheckIfUserExistsOrNot(int userId);
    }
}
