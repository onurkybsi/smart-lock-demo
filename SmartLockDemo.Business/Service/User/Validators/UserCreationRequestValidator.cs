using FluentValidation;
using System.Text.RegularExpressions;

namespace SmartLockDemo.Business.Service.User
{
    internal class UserCreationRequestValidator : AbstractValidator<UserCreationRequest>
    {
        private const string strongPasswordRegexExpression = "(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])(?=.{8,})";

        public UserCreationRequestValidator()
        {
            RuleFor(request => request).NotNull();
            RuleFor(request => request.Email)
                .EmailAddress()
                .Must(email => email.Length < 255);
            RuleFor(request => request.Password)
                .NotEmpty()
                .Must(password => Regex.Match(password, strongPasswordRegexExpression).Success)
                .Must(password => password.Length < 50);
        }
    }
}