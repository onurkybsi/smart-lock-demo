using KybInfrastructure.Data;
using SmartLockDemo.Data.Entites;

namespace SmartLockDemo.Data.Repositories
{
    internal class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(SmartLockDemoDbContext context) : base(context) { }
    }
}
