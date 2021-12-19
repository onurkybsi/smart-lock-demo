using FluentValidation;
using FluentValidation.Results;
using SmartLockDemo.Data;

namespace SmartLockDemo.Business.Service.Administration
{
    internal class DoorAccessCreationRequestValidator : AbstractValidator<DoorAccessCreationRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoorAccessCreationRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            SetValidationRules();
        }

        private void SetValidationRules()
        {
            RuleFor(request => request.DoorId)
                .GreaterThanOrEqualTo(1)
                .Custom((doorId, validationContext) =>
                {
                    if (!_unitOfWork.DoorRepository.CheckIfDoorAlreadyExists(doorId))
                        validationContext
                            .AddFailure(new ValidationFailure("DoorId", "There is no door which has this ID!"));
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
                    if (_unitOfWork.TagDoorRepository.CheckIfAccessAlreadyExistsForThisTag(request.DoorId, request.TagId))
                        validationContext
                            .AddFailure(new ValidationFailure("Request", "This tag has already access to this door!"));
                });
        }
    }
}
