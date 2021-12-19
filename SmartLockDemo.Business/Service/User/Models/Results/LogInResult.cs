using SmartLockDemo.Infrastructure.Utilities;

namespace SmartLockDemo.Business.Service.User
{
    /// <summary>
    /// Represents result of user login operation
    /// </summary>
    public class LogInResult : ResultBase
    {
        public string CreatedToken { get; }

        public LogInResult(bool isSuccessful) : base(isSuccessful) { }

        public LogInResult(bool isSuccessful, string createdToken) : base(isSuccessful) { CreatedToken = createdToken; }

        public LogInResult(bool isSuccessful, string createdToken, object message) : base(isSuccessful, message)
        {
            CreatedToken = createdToken;
        }
    }
}
