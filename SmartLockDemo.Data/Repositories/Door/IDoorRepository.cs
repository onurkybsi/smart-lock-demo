using KybInfrastructure.Data;
using SmartLockDemo.Data.Entities;

namespace SmartLockDemo.Data.Repositories
{
    /// <summary>
    /// Contains various utilities for Door database entity operations
    /// </summary>
    public interface IDoorRepository : IRepository<Door>
    {
        /// <summary>
        /// Checks if there is already a Door with this name
        /// </summary>
        /// <param name="doorName">Door name to check</param>
        /// <returns></returns>
        bool CheckIfDoorAlreadyExists(string doorName);

        /// <summary>
        /// Checks if there is already a Door with this ID
        /// </summary>
        /// <param name="id">Door ID to check</param>
        /// <returns></returns>
        bool CheckIfDoorAlreadyExists(int id);
    }
}
