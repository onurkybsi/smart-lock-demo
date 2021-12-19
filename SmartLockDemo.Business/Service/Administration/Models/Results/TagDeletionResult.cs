using SmartLockDemo.Infrastructure.Utilities;

namespace SmartLockDemo.Business.Service.Administration
{
    /// <summary>
    /// Represents result of tag deletion operation
    /// </summary>
    public class TagDeletionResult : ResultBase
    {
        public TagDeletionResult(bool isSuccessful) : base(isSuccessful) { }

        public TagDeletionResult(bool isSuccessful, object message) : base(isSuccessful, message) { }
    }
}
