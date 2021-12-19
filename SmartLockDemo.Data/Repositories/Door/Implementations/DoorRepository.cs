using KybInfrastructure.Data;
using SmartLockDemo.Data.Entities;
using System;
using System.Linq;

namespace SmartLockDemo.Data.Repositories
{
    internal class DoorRepository : EFRepository<Door>, IDoorRepository
    {
        public DoorRepository(SmartLockDemoDbContext context) : base(context) { }

        public bool CheckIfDoorAlreadyExists(string doorName)
        {
            if (string.IsNullOrWhiteSpace(doorName))
                throw new ArgumentNullException(nameof(doorName));
            return DbSet.Any(door => door.Name == doorName);
        }

        public bool CheckIfDoorAlreadyExists(int id)
            => DbSet.Any(door => door.Id == id);

        public void Delete(int doorId)
        {
            Door entityWillBeDeleted = DbSet.FirstOrDefault(door => door.Id == doorId);
            if (entityWillBeDeleted is null)
                throw new InvalidOperationException("There is no such an entity!");
            DbSet.Remove(entityWillBeDeleted);
        }
    }
}
