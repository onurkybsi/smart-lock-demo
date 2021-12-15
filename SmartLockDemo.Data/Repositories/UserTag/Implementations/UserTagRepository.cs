using KybInfrastructure.Data;
using SmartLockDemo.Data.Entites;

namespace SmartLockDemo.Data.Repositories
{
    internal class UserTagRepository : EFRepository<UserTag>, IUserTagRepository
    {
        public UserTagRepository(SmartLockDemoDbContext context) : base(context) { }
    }
}
