using SmartLockDemo.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLockDemo.Business.Service.SmartLockAdministration
{
    /// <summary>
    /// Represents result of tag creation operation
    /// </summary>
    public class TagCreationResult : ResultBase
    {
        public TagCreationResult(bool isSuccessful) : base(isSuccessful) { }

        public TagCreationResult(bool isSuccessful, object message) : base(isSuccessful, message) { }
    }
}
