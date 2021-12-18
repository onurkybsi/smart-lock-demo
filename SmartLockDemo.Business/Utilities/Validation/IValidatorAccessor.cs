using SmartLockDemo.Business.Service.SmartLockAdministration;
using SmartLockDemo.Business.Service.User;

namespace SmartLockDemo.Business.Utilities
{
    internal interface IValidatorAccessor
    {
        UserCreationRequestValidator UserCreationRequest { get; }
        DoorAccessControlRequestValidator DoorAccessControlRequest { get; }
        DoorCreationRequestValidator DoorCreationRequest { get; }
        TagCreationRequestValidator TagCreationRequest { get; }
        DoorAccessCreationRequestValidator DoorAccessCreationRequest { get; }
        UserTaggingRequestValidator UserTaggingRequest { get; }
    }
}
