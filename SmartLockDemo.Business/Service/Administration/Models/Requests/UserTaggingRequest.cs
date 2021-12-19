using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLockDemo.Business.Service.Administration
{
    /// <summary>
    /// Contains parameters which are for assign a tag to an user
    /// </summary>
    public class UserTaggingRequest
    {
        public int UserId { get; set; }
        public int TagId { get; set; }
    }
}
