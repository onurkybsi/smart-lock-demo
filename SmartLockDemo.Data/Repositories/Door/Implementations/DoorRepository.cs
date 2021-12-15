using KybInfrastructure.Data;
using SmartLockDemo.Data.Entites;

namespace SmartLockDemo.Data.Repositories
{
    internal class DoorRepository : EFRepository<Door>, IDoorRepository
    {
        public DoorRepository(SmartLockDemoDbContext context) : base(context) { }
    }
}
