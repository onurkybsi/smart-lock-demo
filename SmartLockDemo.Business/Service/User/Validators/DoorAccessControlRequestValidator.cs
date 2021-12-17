using FluentValidation;

namespace SmartLockDemo.Business.Service.User
{
    internal class DoorAccessControlRequestValidator : AbstractValidator<DoorAccessControlRequest>
    {
        public DoorAccessControlRequestValidator()
        {
            RuleFor(request => request.UserId)
                .GreaterThanOrEqualTo(1);

            RuleFor(request => request.DoorId)
                .GreaterThanOrEqualTo(1);
        }
    }
}
