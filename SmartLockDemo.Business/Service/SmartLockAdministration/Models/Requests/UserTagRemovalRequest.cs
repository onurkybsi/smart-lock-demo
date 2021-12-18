﻿namespace SmartLockDemo.Business.Service.SmartLockAdministration
{
    /// <summary>
    /// Contains tag removal parameters from a user
    /// </summary>
    public class UserTagRemovalRequest
    {
        public int UserId { get; set; }
        public int TagId { get; set; }
    }
}
