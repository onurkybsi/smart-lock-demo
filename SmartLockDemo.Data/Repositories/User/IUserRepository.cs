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
    }
}
