using SmartLockDemo.Data.Repositories;

namespace SmartLockDemo.Data
{
    internal class UnitOfWork : KybInfrastructure.Data.UnitOfWorkBase<SmartLockDemoDbContext>, IUnitOfWork
    {
        public UnitOfWork(SmartLockDemoDbContext context) : base(context) { }

        private IUserRepository userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository is null)
                    userRepository = new UserRepository(DatabaseContext);
                return userRepository;
            }
        }

        public ITagRepository tagRepository;
        public ITagRepository TagRepository
        {
            get
            {
                if (tagRepository is null)
                    tagRepository = new TagRepository(DatabaseContext);
                return tagRepository;
            }
        }

        private IUserTagRepository userTagRepository;
        public IUserTagRepository UserTagRepository
        {
            get
            {
                if (userTagRepository is null)
                    userTagRepository = new UserTagRepository(DatabaseContext);
                return userTagRepository;
            }
        }

        private IDoorRepository doorRepository;
        public IDoorRepository DoorRepository
        {
            get
            {
                if (doorRepository is null)
                    doorRepository = new DoorRepository(DatabaseContext);
                return doorRepository;
            }
        }

        private ITagDoorRepository tagDoorRepository;
        public ITagDoorRepository TagDoorRepository
        {
            get
            {
                if (tagDoorRepository is null)
                    tagDoorRepository = new TagDoorRepository(DatabaseContext);
                return tagDoorRepository;
            }
        }

        public override int SaveChanges()
            => DatabaseContext.SaveChanges();
    }
}
