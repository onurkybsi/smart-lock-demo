using KybInfrastructure.Data;
using SmartLockDemo.Data.Entities;
using System;
using System.Linq;

namespace SmartLockDemo.Data.Repositories
{
    internal class UserTagRepository : EFRepository<UserTag>, IUserTagRepository
    {
        public UserTagRepository(SmartLockDemoDbContext context) : base(context) { }

        public IQueryable<UserTag> GetTable()
            => DbSet.AsQueryable();

        public bool CheckIfUserAlreadyHasThisTag(int userId, int tagId)
            => DbSet.Any(userTag => userTag.UserId == userId &&
                                    userTag.TagId == tagId);

        // TO-DO: This approach is not optimal, entity shouldn't be loaded to memory!
        public void Remove(int userId, int doorId)
        {
            UserTag entityWillBeDeleted = DbSet.FirstOrDefault(userTag =>
                userTag.UserId == userId &&
                userTag.TagId == doorId);
            if (entityWillBeDeleted is null)
                throw new InvalidOperationException("There is no such an entity!");
            DbSet.Remove(entityWillBeDeleted);
        }
    }
}
