using KybInfrastructure.Data;
using SmartLockDemo.Data.Entities;
using System.Linq;

namespace SmartLockDemo.Data.Repositories
{
    /// <summary>
    /// Contains various utilities for UserTag database entity operations
    /// </summary>
    public interface IUserTagRepository : IRepository<UserTag>
    {
        /// <summary>
        /// Receives IQueryable collection which represents database table for customer queries
        /// </summary>
        /// <returns>IQueryable collection of UserTag database table</returns>
        IQueryable<UserTag> GetTable();
    }
}
