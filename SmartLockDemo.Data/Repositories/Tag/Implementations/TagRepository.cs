using KybInfrastructure.Data;
using SmartLockDemo.Data.Entites;

namespace SmartLockDemo.Data.Repositories
{
    internal class TagRepository : EFRepository<Tag>, ITagRepository
    {
        public TagRepository(SmartLockDemoDbContext context) : base(context) { }
    }
}
