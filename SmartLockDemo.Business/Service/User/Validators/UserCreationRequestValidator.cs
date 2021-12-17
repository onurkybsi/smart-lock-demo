using FluentValidation;
using FluentValidation.Results;
using SmartLockDemo.Data;

namespace SmartLockDemo.Business.Service.User
{
    internal class UserCreationRequestValidator : AbstractValidator<UserCreationRequest>
    {
        private const string STRONG_PASSWORD_REGEX_EXPRESSION = "(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])(?=.{8,})";

        private readonly IUnitOfWork _unitOfWork;

        public UserCreationRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            SetValidationRules();
        }

        private void SetValidationRules()
        {
            RuleFor(request => request.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255)
                .Custom((email, validationContext) =>
                {
                    if (_unitOfWork.UserRepository.CheckIfEmailAlreadyExists(email))
                        validationContext
                            .AddFailure(new ValidationFailure("Email", "This email already exists!"));
                });
            RuleFor(request => request.Password)
                .NotEmpty()
                .Matches(STRONG_PASSWORD_REGEX_EXPRESSION)
                .MaximumLength(255);
        }
    }
}