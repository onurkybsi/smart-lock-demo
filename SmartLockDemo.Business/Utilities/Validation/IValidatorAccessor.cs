﻿using SmartLockDemo.Business.Service.User;

namespace SmartLockDemo.Business.Utilities
{
    internal interface IValidatorAccessor
    {
        UserCreationRequestValidator UserCreationRequest { get; }
        DoorAccessControlRequestValidator DoorAccessControlRequest { get; }
    }
}