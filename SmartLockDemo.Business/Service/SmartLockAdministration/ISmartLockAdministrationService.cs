﻿using SmartLockDemo.Business.Service.User;

namespace SmartLockDemo.Business.Service.SmartLockAdministration
{
    public interface ISmartLockAdministrationService
    {
        UserCreationResult CreateUser(UserCreationRequest request);
    }
}