using FluentValidation;
using FluentValidation.Results;
using SmartLockDemo.Data;

namespace SmartLockDemo.Business.Service.SmartLockAdministration
{
    internal class DoorCreationRequestValidator : AbstractValidator<DoorCreationRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoorCreationRequestValidator(IUnitOfWork unitOfWork)
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
                    if (_unitOfWork.DoorRepository.CheckIfDoorAlreadyExists(name))
                        validationContext
                            .AddFailure(new ValidationFailure("Name", "This door already exists!"));
                });
        }
    }
}
