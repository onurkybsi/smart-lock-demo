using FluentValidation;
using FluentValidation.Results;
using SmartLockDemo.Data;

namespace SmartLockDemo.Business.Service.Administration
{
    internal class TagDeletionRequestValidator : AbstractValidator<TagDeletionRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagDeletionRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            SetValidationRules();
        }

        private void SetValidationRules()
        {
            RuleFor(request => request.TagId)
                .GreaterThanOrEqualTo(1)
                .Custom((tagId, validationContext) =>
                {
                    if (!_unitOfWork.TagRepository.CheckIfTagAlreadyExists(tagId))
                        validationContext
                            .AddFailure(new ValidationFailure("Name", "There is no such a tag already!"));
                });
        }
    }
}
