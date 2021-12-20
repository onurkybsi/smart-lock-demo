using SmartLockDemo.Business.Service.Administration;
using System.Collections.Generic;

namespace SmartLockDemo.Business.Service.User
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
