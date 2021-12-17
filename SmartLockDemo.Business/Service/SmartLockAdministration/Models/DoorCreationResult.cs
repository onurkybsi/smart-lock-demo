using SmartLockDemo.Infrastructure.Utilities;

namespace SmartLockDemo.Business.Service.SmartLockAdministration
{
    /// <summary>
    /// Represents result of door creation operation
    /// </summary>
    public class DoorCreationResult : ResultBase
    {
        public DoorCreationResult(bool isSuccessful) : base(isSuccessful) { }
        public DoorCreationResult(bool isSuccessful, object message) : base(isSuccessful, message) { }
    }
}
