using FluentValidation;
using FluentValidation.Results;
using SmartLockDemo.Data;

namespace SmartLockDemo.Business.Service.Administration
{
    public class UserTagRemovalRequestValidator : AbstractValidator<UserTagRemovalRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserTagRemovalRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            SetValidationRules();
        }

        private void SetValidationRules()
        {
            RuleFor(request => request.TagId)
                .GreaterThanOrEqualTo(1);
            RuleFor(request => request.UserId)
                .GreaterThanOrEqualTo(1);
            RuleFor(request => request)
                .Custom((request, validationContext) =>
                {
                    if (!_unitOfWork.UserTagRepository.CheckIfUserAlreadyHasThisTag(request.UserId, request.TagId))
                        validationContext
                            .AddFailure(new ValidationFailure("Request", "There is no such a tag on this user!"));
                });
        }
    }
}
