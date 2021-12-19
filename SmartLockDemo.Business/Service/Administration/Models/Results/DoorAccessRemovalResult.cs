using SmartLockDemo.Infrastructure.Utilities;

namespace SmartLockDemo.Business.Service.Administration
{
    /// <summary>
    /// Represents result of door access removal operation
    /// </summary>
    public class DoorAccessRemovalResult : ResultBase
    {
        public DoorAccessRemovalResult(bool isSuccessful) : base(isSuccessful) { }

        public DoorAccessRemovalResult(bool isSuccessful, object message) : base(isSuccessful, message) { }
    }
}
