using System.Collections.Generic;

namespace SmartLockDemo.Data.Entities
{
    public partial class Door
    {
        public Door()
        {
            TagDoors = new HashSet<TagDoor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TagDoor> TagDoors { get; set; }
    }
}
