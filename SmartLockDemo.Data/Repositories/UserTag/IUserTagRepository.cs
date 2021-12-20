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

        /// <summary>
        /// Checks if the user already has this tag or not
        /// </summary>
        /// <param name="userId">Id of user will be checked if it has the tag or not</param>
        /// <param name="tagId">Id of tag will be checked if it's already assigned to the user or not</param>
        /// <returns></returns>
        bool CheckIfUserAlreadyHasThisTag(int userId, int tagId);

        /// <summary>
        /// Removes UserTag entity by userId and tagId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tagId"></param>
        void Remove(int userId, int tagId);
    }
}
