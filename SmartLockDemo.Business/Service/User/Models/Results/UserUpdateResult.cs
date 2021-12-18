using SmartLockDemo.Infrastructure.Utilities;

namespace SmartLockDemo.Business.Service.User
{
    /// <summary>
    /// Represents user update operation result
    /// </summary>
    public class UserUpdateResult : ResultBase
    {
        public UserUpdateResult(bool isSuccessful) : base(isSuccessful) { }

        public UserUpdateResult(bool isSuccessful, object message) : base(isSuccessful, message) { }
    }
}
