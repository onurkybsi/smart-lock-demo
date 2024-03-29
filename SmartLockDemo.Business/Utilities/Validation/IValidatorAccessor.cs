﻿using SmartLockDemo.Business.Service.Administration;
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
        DoorAccessRemovalRequestValidator DoorAccessRemovalRequest { get; }
        UserTagRemovalRequestValidator UserTagRemovalRequest { get; }
        UserDeletionRequestValidator UserDeletionRequest { get; }
        DoorDeletionRequestValidator DoorDeletionRequest { get; }
        TagDeletionRequestValidator TagDeletionRequest { get; }
        UserUpdateRequestValidator UserUpdateRequest { get; }
        LogInRequestValidator LogInRequest { get; }
    }
}
