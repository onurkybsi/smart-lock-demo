using FluentValidation;
using FluentValidation.Results;
using SmartLockDemo.Data;

namespace SmartLockDemo.Business.Service.Administration
{
    internal class DoorDeletionRequestValidator : AbstractValidator<DoorDeletionRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoorDeletionRequestValidator(IUnitOfWork unitOfWork)
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
                            .AddFailure(new ValidationFailure("Name", "There is no such a door already!"));
                });
        }
    }
}
