﻿using KybInfrastructure.Data;
using SmartLockDemo.Data.Entities;
using System.Collections.Generic;

namespace SmartLockDemo.Data.Repositories
{
    /// <summary>
    /// Contains various utilities for Tag database entity operations
    /// </summary>
    public interface ITagRepository : IRepository<Tag>
    {
        /// <summary>
        /// Checks if there is already a Tag with this name
        /// </summary>
        /// <param name="tagName">Tag name to check</param>
        /// <returns></returns>
        bool CheckIfTagAlreadyExists(string tagName);

        /// <summary>
        /// Checks if there is already a Tag with this ID
        /// </summary>
        /// <param name="id">Tag ID to check</param>
        /// <returns></returns>
        bool CheckIfTagAlreadyExists(int id);

        /// <summary>
        /// Deletes a tag by given ID
        /// </summary>
        /// <param name="tagId"></param>
        void Delete(int tagId);

        /// <summary>
        /// Receives all tags from the repository
        /// </summary>
        /// <returns></returns>
        List<Tag> GetAllTags();
    }
}
