using FluentValidation;
using FluentValidation.Results;
using SmartLockDemo.Data;

namespace SmartLockDemo.Business.Service.Administration
{
    internal class DoorAccessRemovalRequestValidator : AbstractValidator<DoorAccessRemovalRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoorAccessRemovalRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            SetValidationRules();
        }

        private void SetValidationRules()
        {
            RuleFor(request => request.DoorId)
                .GreaterThanOrEqualTo(1);
            RuleFor(request => request.TagId)
                .GreaterThanOrEqualTo(1);
            RuleFor(request => request)
                .Custom((request, validationContext) =>
                {
                    if (!_unitOfWork.TagDoorRepository.CheckIfAccessAlreadyExistsForThisTag(request.DoorId, request.TagId))
                        validationContext
                            .AddFailure(new ValidationFailure("Request", "There is no such an access!"));
                });
        }
    }
}
