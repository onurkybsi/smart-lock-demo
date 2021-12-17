using System.Collections.Generic;

namespace SmartLockDemo.Data.Entities
{
    public partial class Tag
    {
        public Tag()
        {
            TagDoors = new HashSet<TagDoor>();
            UserTags = new HashSet<UserTag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TagDoor> TagDoors { get; set; }
        public virtual ICollection<UserTag> UserTags { get; set; }
    }
}
