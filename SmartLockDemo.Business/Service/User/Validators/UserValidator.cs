using FluentValidation;

namespace SmartLockDemo.Business.Service.User
{
    internal class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user).NotNull();
        }
    }
}
