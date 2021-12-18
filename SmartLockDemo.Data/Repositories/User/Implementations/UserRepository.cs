using KybInfrastructure.Data;
using SmartLockDemo.Data.Entities;
using System;
using System.Linq;

namespace SmartLockDemo.Data.Repositories
{
    internal class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(SmartLockDemoDbContext context) : base(context) { }

        public bool CheckIfEmailAlreadyExists(string emailToCheck)
        {
            if (string.IsNullOrWhiteSpace(emailToCheck))
                throw new ArgumentNullException(nameof(emailToCheck));
            return DbSet.Any(user => user.Email == emailToCheck);
        }

        public bool CheckIfUserExistsOrNot(int userId)
            => DbSet.Any(user => user.Id == userId);

        public void Delete(int userId)
        {
            User entityWillBeDeleted = DbSet.FirstOrDefault(user => user.Id == userId);
            if (entityWillBeDeleted is null)
                throw new InvalidOperationException("There is no such an entity!");
            DbSet.Remove(entityWillBeDeleted);
        }
    }
}
