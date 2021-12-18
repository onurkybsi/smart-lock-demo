using SmartLockDemo.Infrastructure.Utilities;

namespace SmartLockDemo.Business.Service.SmartLockAdministration
{
    /// <summary>
    /// Represents result of user deletion operation
    /// </summary>
    public class UserDeletionResult : ResultBase
    {
        public UserDeletionResult(bool isSuccessful) : base(isSuccessful) { }

        public UserDeletionResult(bool isSuccessful, object message) : base(isSuccessful, message) { }
    }
}
