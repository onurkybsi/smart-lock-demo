using SmartLockDemo.Data.Repositories;

namespace SmartLockDemo.Data
{
    /// <summary>
    /// Manages database operations, provides access to repositories
    /// </summary>
    public interface IUnitOfWork : KybInfrastructure.Data.IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public ITagRepository TagRepository { get; }
        public IUserTagRepository UserTagRepository { get; }
        public IDoorRepository DoorRepository { get; }
        public ITagDoorRepository TagDoorRepository { get; }
    }
}
