using KybInfrastructure.Data;
using SmartLockDemo.Data.Entities;
using System.Collections.Generic;

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

        /// <summary>
        /// Deletes user by ID
        /// </summary>
        /// <param name="userId"></param>
        void Delete(int userId);

        /// <summary>
        /// Receives hashed password of the user by given email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Hashed password of user</returns>
        string GetHashedPasswordByEmail(string email);

        /// <summary>
        /// Receives all users from the repository
        /// </summary>
        /// <returns></returns>
        List<User> GetAllUsers();
    }
}
