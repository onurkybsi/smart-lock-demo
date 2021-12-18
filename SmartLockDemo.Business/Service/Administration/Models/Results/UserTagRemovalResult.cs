using SmartLockDemo.Infrastructure.Utilities;

namespace SmartLockDemo.Business.Service.SmartLockAdministration
{
    /// <summary>
    /// Represents tag removal operation of user
    /// </summary>
    public class UserTagRemovalResult : ResultBase
    {
        public UserTagRemovalResult(bool isSuccessful) : base(isSuccessful) { }

        public UserTagRemovalResult(bool isSuccessful, object message) : base(isSuccessful, message) { }
    }
}
