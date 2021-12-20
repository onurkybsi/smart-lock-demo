using Microsoft.Extensions.Logging;
using SmartLockDemo.Data.Repositories;
using SmartLockDemo.Data.Utilities;

namespace SmartLockDemo.Data
{
    internal class UnitOfWork : KybInfrastructure.Data.UnitOfWorkBase<SmartLockDemoDbContext>, IUnitOfWork
    {
        private readonly IRedisClient _redisClient;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(SmartLockDemoDbContext context,
            IRedisClient redisClient, ILogger<UnitOfWork> logger) : base(context)
        {
            _redisClient = redisClient;
            _logger = logger;
        }

        private IUserRepository userRepository;
        public IUserRepository UserRepository {
            get {
                if (userRepository is null)
                    userRepository = new UserRepository(DatabaseContext);
                return userRepository;
            }
        }

        public ITagRepository tagRepository;
        public ITagRepository TagRepository {
            get {
                if (tagRepository is null)
                    tagRepository = new TagRepository(DatabaseContext);
                return tagRepository;
            }
        }

        private IUserTagRepository userTagRepository;
        public IUserTagRepository UserTagRepository {
            get {
                if (userTagRepository is null)
                    userTagRepository = new UserTagRepository(DatabaseContext, UserDoorAccessRepository);
                return userTagRepository;
            }
        }

        private IDoorRepository doorRepository;
        public IDoorRepository DoorRepository {
            get {
                if (doorRepository is null)
                    doorRepository = new DoorRepository(DatabaseContext);
                return doorRepository;
            }
        }

        private ITagDoorRepository tagDoorRepository;
        public ITagDoorRepository TagDoorRepository {
            get {
                if (tagDoorRepository is null)
                    tagDoorRepository = new TagDoorRepository(DatabaseContext);
                return tagDoorRepository;
            }
        }

        private IUserDoorAccessRepository userDoorAccessRepository;
        public IUserDoorAccessRepository UserDoorAccessRepository {
            get {
                if (userDoorAccessRepository is null)
                    userDoorAccessRepository = new UserDoorAccessRepository(_redisClient, DatabaseContext, _logger);
                return userDoorAccessRepository;
            }
        }

        public override int SaveChanges()
            => DatabaseContext.SaveChanges();
    }
}
