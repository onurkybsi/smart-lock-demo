using SmartLockDemo.Data.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLockDemo.Data.Repositories
{
    internal class UserDoorAccessRepository : IUserDoorAccessRepository
    {
        private const string TRUE_BIT = "1";
        private const string FALSE_BIT = "0";

        private readonly IRedisClient _redisClient;
        private readonly SmartLockDemoDbContext _context;

        public UserDoorAccessRepository(IRedisClient redisClient, SmartLockDemoDbContext context)
        {
            _redisClient = redisClient;
            _context = context;
        }

        public bool CheckThatUserHasAccessTheDoor(int userId, int doorId)
        {
            if (userId <= 0)
                throw new InvalidOperationException("userId cannot be less than or equal to zero!");
            if (doorId <= 0)
                throw new InvalidOperationException("userId cannot be less than or equal to zero!");

            bool? cacheControlResult = CheckThatUserHasAccessTheDoorFromCache(userId, doorId);
            if (cacheControlResult.HasValue)
                return cacheControlResult.Value;

            bool doesUserHasAccess = CheckThatUserHasAccessTheDoorFromSqlDb(userId, doorId);
            CacheReceivedAccessInfo(userId, doorId, doesUserHasAccess);

            return doesUserHasAccess;
        }

        private bool? CheckThatUserHasAccessTheDoorFromCache(int userId, int doorId)
        {
            string value = _redisClient.Get($"{userId}{doorId}");
            if (value != null)
                return value == TRUE_BIT;
            return default;
        }

        private bool CheckThatUserHasAccessTheDoorFromSqlDb(int userId, int doorId)
            => (from td in _context.TagDoors
                join ut in _context.UserTags on td.TagId equals ut.TagId
                where td.DoorId == userId && ut.UserId == doorId
                select new { }).Any();

        private void CacheReceivedAccessInfo(int userId, int doorId, bool doesUserHasAccess)
            => Task.Factory.StartNew(() =>
                _redisClient.Set($"{userId}{doorId}", doesUserHasAccess ? TRUE_BIT : FALSE_BIT)
            );
    }
}
