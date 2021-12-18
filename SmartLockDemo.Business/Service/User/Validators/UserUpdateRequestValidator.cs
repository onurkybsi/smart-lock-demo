using FluentValidation;
using FluentValidation.Results;
using SmartLockDemo.Data;

namespace SmartLockDemo.Business.Service.User
{
    public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
    {
        private const string STRONG_PASSWORD_REGEX_EXPRESSION = "(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])(?=.{8,})";

        private readonly IUnitOfWork _unitOfWork;

        public UserUpdateRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            SetValidationRules();
        }

        private void SetValidationRules()
        {
            RuleFor(request => request.Id)
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .Custom((id, validationContext) =>
                {
                    if (!id.HasValue)
                        return;
                    if (!_unitOfWork.UserRepository.CheckIfUserExistsOrNot(id.GetValueOrDefault()))
                        validationContext
                            .AddFailure(new ValidationFailure("Id", "There is no such a user!"));
                });
            RuleFor(request => request.Email)
                .EmailAddress()
                .MaximumLength(255)
                .Custom((email, validationContext) =>
                {
                    if (_unitOfWork.UserRepository.CheckIfEmailAlreadyExists(email))
                        validationContext
                            .AddFailure(new ValidationFailure("Email", "This email already exists!"));
                })
                .When(request => request.Email != null);
            RuleFor(request => request.Password)
                .Matches(STRONG_PASSWORD_REGEX_EXPRESSION)
                .MaximumLength(255)
                .When(request => request.Password != null);
        }
    }
}
