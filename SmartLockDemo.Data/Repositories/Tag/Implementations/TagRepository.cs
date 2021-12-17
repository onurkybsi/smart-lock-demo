using KybInfrastructure.Data;
using SmartLockDemo.Data.Entities;
using System;
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
    }
}
