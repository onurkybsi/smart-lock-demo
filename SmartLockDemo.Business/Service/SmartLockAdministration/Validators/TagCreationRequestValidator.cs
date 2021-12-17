using FluentValidation;
using FluentValidation.Results;
using SmartLockDemo.Data;

namespace SmartLockDemo.Business.Service.SmartLockAdministration
{
    internal class TagCreationRequestValidator : AbstractValidator<TagCreationRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagCreationRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            SetValidationRules();
        }

        private void SetValidationRules()
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .MaximumLength(50)
                .Custom((name, validationContext) =>
                {
                    if (_unitOfWork.TagRepository.CheckIfTagAlreadyExists(name))
                        validationContext
                            .AddFailure(new ValidationFailure("Name", "This tag already exists!"));
                });
        }
    }
}
