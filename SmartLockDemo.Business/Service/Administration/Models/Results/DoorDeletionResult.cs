using SmartLockDemo.Infrastructure.Utilities;

namespace SmartLockDemo.Business.Service.Administration
{
    /// <summary>
    /// Represents result of door deletion operation
    /// </summary>
    public class DoorDeletionResult : ResultBase
    {
        public DoorDeletionResult(bool isSuccessful) : base(isSuccessful) { }

        public DoorDeletionResult(bool isSuccessful, object message) : base(isSuccessful, message) { }
    }
}
