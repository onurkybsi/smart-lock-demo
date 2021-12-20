using KybInfrastructure.Data;
using SmartLockDemo.Data.Entities;
using System;
using System.Linq;

namespace SmartLockDemo.Data.Repositories
{
    internal class UserTagRepository : EFRepository<UserTag>, IUserTagRepository
    {
        private readonly IUserDoorAccessRepository _userDoorAccessRepository;

        public UserTagRepository(SmartLockDemoDbContext context, IUserDoorAccessRepository userDoorAccessRepository) : base(context)
        {
            _userDoorAccessRepository = userDoorAccessRepository;
        }

        public IQueryable<UserTag> GetTable()
            => DbSet.AsQueryable();

        public bool CheckIfUserAlreadyHasThisTag(int userId, int tagId)
            => DbSet.Any(userTag => userTag.UserId == userId &&
                                    userTag.TagId == tagId);

        public new void Add(UserTag userTag)
        {
            if (userTag is null)
                throw new ArgumentNullException(nameof(userTag));
            DbSet.Add(userTag);
            _userDoorAccessRepository.TryToSetAccessibilityOfUserInCache(userTag.UserId, userTag.TagId, true);
        }

        // TO-DO: This approach is not optimal, entity shouldn't be loaded to memory!
        public void Remove(int userId, int tagId)
        {
            UserTag entityWillBeDeleted = DbSet.FirstOrDefault(userTag =>
                userTag.UserId == userId &&
                userTag.TagId == tagId);
            if (entityWillBeDeleted is null)
                throw new InvalidOperationException("There is no such an entity!");

            DbSet.Remove(entityWillBeDeleted);

            _userDoorAccessRepository.TryToSetAccessibilityOfUserInCache(userId, tagId, false);
        }
    }
}
