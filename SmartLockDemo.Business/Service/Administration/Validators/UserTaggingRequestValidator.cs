using FluentValidation;
using FluentValidation.Results;
using SmartLockDemo.Data;

namespace SmartLockDemo.Business.Service.Administration
{
    internal class UserTaggingRequestValidator : AbstractValidator<UserTaggingRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserTaggingRequestValidator(IUnitOfWork unitOfWork)
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
                               .AddFailure(new ValidationFailure("UserId", "There is no user which has this ID!"));
                   });
            RuleFor(request => request.TagId)
                .GreaterThanOrEqualTo(1)
                .Custom((tagId, validationContext) =>
                {
                    if (!_unitOfWork.TagRepository.CheckIfTagAlreadyExists(tagId))
                        validationContext
                            .AddFailure(new ValidationFailure("TagId", "There is no tag which has this ID!"));
                });
            RuleFor(request => request)
                .Custom((request, validationContext) =>
                {
                    if (_unitOfWork.UserTagRepository.CheckIfUserAlreadyHasThisTag(request.UserId, request.TagId))
                        validationContext
                            .AddFailure(new ValidationFailure("Request", "This user has already this tag!"));
                });
        }
    }
}
