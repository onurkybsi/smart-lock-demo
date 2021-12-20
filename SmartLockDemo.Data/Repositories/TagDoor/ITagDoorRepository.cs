using KybInfrastructure.Data;
using SmartLockDemo.Data.Entities;
using System.Collections.Generic;
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

        /// <summary>
        /// Checks if the door is already accessible for the tag
        /// </summary>
        /// <param name="doorId">Id of door will be checked if it's accessible or not</param>
        /// <param name="tagId">Id of tag will be checked if it already has access the door or not</param>
        /// <returns></returns>
        bool CheckIfAccessAlreadyExistsForThisTag(int doorId, int tagId);

        /// <summary>
        /// Removes TagDoor entity by tagId and doorId
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="doorId"></param>
        void Remove(int tagId, int doorId);
    }
}
