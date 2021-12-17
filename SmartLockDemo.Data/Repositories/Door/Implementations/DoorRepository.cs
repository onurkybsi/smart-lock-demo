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
    }
}
