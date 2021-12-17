using System.Collections.Generic;

namespace SmartLockDemo.Data.Entities
{
    public partial class User
    {
        public User()
        {
            UserTags = new HashSet<UserTag>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public byte Role { get; set; }
        public string AuthorizationToken { get; set; }

        public virtual ICollection<UserTag> UserTags { get; set; }
    }
}
