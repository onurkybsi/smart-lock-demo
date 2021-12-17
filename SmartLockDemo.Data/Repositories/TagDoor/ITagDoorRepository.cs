using KybInfrastructure.Data;
using SmartLockDemo.Data.Entities;
using System.Linq;

namespace SmartLockDemo.Data.Repositories
{
    /// <summary>
    /// Contains various utilities for TagDoor database entity operations
    /// </summary>
    public interface ITagDoorRepository : IRepository<TagDoor>
    {
        /// <summary>
        /// Receives IQueryable collection which represents database table for customer queries
        /// </summary>
        /// <returns>IQueryable collection of TagDoor database table</returns>
        IQueryable<TagDoor> GetTable();
    }
}
