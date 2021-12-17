using KybInfrastructure.Data;
using SmartLockDemo.Data.Entities;
using System.Linq;

namespace SmartLockDemo.Data.Repositories
{
    internal class UserTagRepository : EFRepository<UserTag>, IUserTagRepository
    {
        public UserTagRepository(SmartLockDemoDbContext context) : base(context) { }

        public IQueryable<UserTag> GetTable()
            => DbSet.AsQueryable();
    }
}
