using SmartLockDemo.Business.Service.User;

namespace SmartLockDemo.Business.Service.SmartLockAdministration
{
    public interface ISmartLockAdministrationService
    {
        UserCreationResult CreateUser(UserCreationRequest request);
        DoorCreationResult CreateDoor(DoorCreationRequest request);
        TagCreationResult CreateTag(TagCreationRequest request);
        DoorAccessCreationResult CreateDoorAccess(DoorAccessCreationRequest request);
        UserTaggingResult TagUser(UserTaggingRequest request);
    }
}
