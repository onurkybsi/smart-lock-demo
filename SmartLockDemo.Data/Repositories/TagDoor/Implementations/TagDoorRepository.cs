using KybInfrastructure.Data;
using SmartLockDemo.Data.Entites;

namespace SmartLockDemo.Data.Repositories
{
    internal class TagDoorRepository : EFRepository<TagDoor>, ITagDoorRepository
    {
        public TagDoorRepository(SmartLockDemoDbContext context) : base(context) { }
    }
}
