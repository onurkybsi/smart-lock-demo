using KybInfrastructure.Data;
using SmartLockDemo.Data.Entities;
using System;
using System.Linq;

namespace SmartLockDemo.Data.Repositories
{
    internal class TagDoorRepository : EFRepository<TagDoor>, ITagDoorRepository
    {
        public TagDoorRepository(SmartLockDemoDbContext context) : base(context) { }

        public IQueryable<TagDoor> GetTable()
            => DbSet.AsQueryable();

        public bool CheckIfAccessAlreadyExistsForThisTag(int doorId, int tagId)
            => DbSet.Any(tagDoor => tagDoor.DoorId == doorId &&
                                    tagDoor.TagId == tagId);

        // TO-DO: This approach is not optimal, entity shouldn't be loaded to memory!
        public void Remove(int tagId, int doorId)
        {
            TagDoor entityWillBeDeleted = DbSet.FirstOrDefault(tagDoor =>
                tagDoor.TagId == tagId &&
                tagDoor.DoorId == doorId);
            if (entityWillBeDeleted is null)
                throw new InvalidOperationException("There is no such an entity!");
            DbSet.Remove(entityWillBeDeleted);
        }
    }
}
