using FluentValidation.Results;
using SmartLockDemo.Infrastructure.Utilities;

namespace SmartLockDemo.Business.Service.User
{
    /// <summary>
    /// Represents User creation operation result
    /// </summary>
    public class UserCreationResult : ResultBase
    {
        public UserCreationResult(bool isSuccessful) : base(isSuccessful, "User creation is completed successfully!") { }

        public UserCreationResult(bool isSuccessful, string message) : base(isSuccessful, message) { }

        public UserCreationResult(ValidationResult validationResult) : base(validationResult.IsValid,
            validationResult.ExtractErrorMessagesFromValidationResult())
        { }
    }
}
