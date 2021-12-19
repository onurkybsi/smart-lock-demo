using SmartLockDemo.Infrastructure.Utilities;

namespace SmartLockDemo.Business.Service.Administration
{
    /// <summary>
    /// Represents user tagging operation result
    /// </summary>
    public class UserTaggingResult : ResultBase
    {
        public UserTaggingResult(bool isSuccessful) : base(isSuccessful) { }

        public UserTaggingResult(bool isSuccessful, object message) : base(isSuccessful, message) { }
    }
}
