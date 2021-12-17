using KybInfrastructure.Data;
using SmartLockDemo.Data.Entities;
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
    }
}
