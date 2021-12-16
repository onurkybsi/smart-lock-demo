using KybInfrastructure.Data;
using SmartLockDemo.Data.Entites;
using System.Linq;

namespace SmartLockDemo.Data.Repositories
{
    internal class TagDoorRepository : EFRepository<TagDoor>, ITagDoorRepository
    {
        public TagDoorRepository(SmartLockDemoDbContext context) : base(context) { }

        public IQueryable<TagDoor> GetTable()
            => DbSet.AsQueryable();
    }
}
