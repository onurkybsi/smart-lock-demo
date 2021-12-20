using Microsoft.Extensions.Logging;
using SmartLockDemo.Data.Utilities;
using System;
using System.Collections.Generic;
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
        private readonly ILogger<UnitOfWork> _logger;

        public UserDoorAccessRepository(IRedisClient redisClient, SmartLockDemoDbContext context,
            ILogger<UnitOfWork> logger)
        {
            _redisClient = redisClient;
            _context = context;
            _logger = logger;
        }

        public bool CheckThatUserHasAccessTheDoor(int userId, int doorId)
        {
            if (userId <= 0)
                throw new InvalidOperationException("userId cannot be less than or equal to zero!");
            if (doorId <= 0)
                throw new InvalidOperationException("doorId cannot be less than or equal to zero!");

            if (ModuleDescriptor.moduleContext.IsCachingActive)
            {
                bool? cacheControlResult = CheckThatUserHasAccessTheDoorFromCache(userId, doorId);
                if (cacheControlResult.HasValue)
                    return cacheControlResult.Value;
            }

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

        public void TryToSetAccessibilityOfUserInCache(int userId, int tagId, bool canUserAccessDoorsOfThisTag)
        {
            if (userId <= 0 || tagId <= 0)
                throw new InvalidOperationException("userId and tagId cannot be less than or equal to zero!");
            if (!ModuleDescriptor.moduleContext.IsCachingActive)
                return;

            List<int> doorIdsThatUserCouldAccessWithTheTag = GetDoorIdsByTagId(tagId);

            int numberOfAttempsToSetCache = 3;
            while (numberOfAttempsToSetCache > 0)
            {
                numberOfAttempsToSetCache--;

                try
                {
                    doorIdsThatUserCouldAccessWithTheTag.ForEach(doorIdThatUserCouldAccessWithTheTag =>
                        _redisClient.Set($"{userId}{doorIdThatUserCouldAccessWithTheTag}",
                            canUserAccessDoorsOfThisTag ? TRUE_BIT : FALSE_BIT));
                    break;
                }
                catch (Exception ex)
                {
                    if (numberOfAttempsToSetCache == 0)
                    {
                        _logger.LogError($"Cache update couldn't complete! Cache control being canceled... {ex}");
                        ModuleDescriptor.moduleContext.IsCachingActive = false;
                    }
                    else
                        _logger.LogError($"Cache update is unsuccessful, trying again... {ex}");
                }
            }
        }

        private List<int> GetDoorIdsByTagId(int tagId)
            => _context.TagDoors.Where(tagDoor => tagDoor.TagId == tagId)
                            .Select(tagDoor => tagDoor.DoorId)
                            .ToList() ?? new List<int>();
    }
}
