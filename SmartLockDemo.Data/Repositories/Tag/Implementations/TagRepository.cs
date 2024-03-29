﻿using KybInfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SmartLockDemo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartLockDemo.Data.Repositories
{
    internal class TagRepository : EFRepository<Tag>, ITagRepository
    {
        public TagRepository(SmartLockDemoDbContext context) : base(context) { }

        public bool CheckIfTagAlreadyExists(string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName))
                throw new ArgumentNullException(nameof(tagName));
            return DbSet.Any(tag => tag.Name == tagName);
        }

        public bool CheckIfTagAlreadyExists(int id)
            => DbSet.Any(tag => tag.Id == id);

        public void Delete(int tagId)
        {
            Tag entityWillBeDeleted = DbSet.FirstOrDefault(tag => tag.Id == tagId);
            if (entityWillBeDeleted is null)
                throw new InvalidOperationException("There is no such an entity!");
            DbSet.Remove(entityWillBeDeleted);
        }

        public List<Tag> GetAllTags()
            => (from tag in DbSet
                select tag).Include(tag => tag.TagDoors)
                           .ThenInclude(tagDoor => tagDoor.Door)
                           .ToList();
    }
}
