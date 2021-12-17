using FluentValidation;

namespace SmartLockDemo.Business.Service.User
{
    internal class UserCreationRequestValidator : AbstractValidator<UserCreationRequest>
    {
        private const string strongPasswordRegexExpression = "(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])(?=.{8,})";

        public UserCreationRequestValidator()
        {
            RuleFor(request => request.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);
            RuleFor(request => request.Password)
                .NotEmpty()
                .Matches(strongPasswordRegexExpression)
                .MaximumLength(255);
        }
    }
}