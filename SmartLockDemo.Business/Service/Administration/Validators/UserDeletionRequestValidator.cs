using FluentValidation;
using FluentValidation.Results;
using SmartLockDemo.Data;

namespace SmartLockDemo.Business.Service.SmartLockAdministration
{
    internal class UserDeletionRequestValidator : AbstractValidator<UserDeletionRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserDeletionRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            SetValidationRules();
        }

        private void SetValidationRules()
        {
            RuleFor(request => request.UserId)
                .GreaterThanOrEqualTo(1)
                .Custom((userId, validationContext) =>
                {
                    if (!_unitOfWork.UserRepository.CheckIfUserExistsOrNot(userId))
                        validationContext
                            .AddFailure(new ValidationFailure("Name", "There is no such a user already!"));
                });
        }
    }
}
