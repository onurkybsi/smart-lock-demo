using SmartLockDemo.Infrastructure.Utilities;

namespace SmartLockDemo.Business.Service.SmartLockAdministration
{
    /// <summary>
    /// Represents result of door access creation operation
    /// </summary>
    public class DoorAccessCreationResult : ResultBase
    {
        public DoorAccessCreationResult(bool isSuccessful) : base(isSuccessful) { }

        public DoorAccessCreationResult(bool isSuccessful, object message) : base(isSuccessful, message) { }
    }
}
