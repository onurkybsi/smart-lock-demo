﻿using KybInfrastructure.Data;
using SmartLockDemo.Data.Entites;
using System;
using System.Linq;

namespace SmartLockDemo.Data.Repositories
{
    internal class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(SmartLockDemoDbContext context) : base(context) { }

        public bool CheckIfEmailAlreadyExists(string emailToCheck)
        {
            if (string.IsNullOrWhiteSpace(emailToCheck))
                throw new ArgumentNullException(nameof(emailToCheck));
            return DbSet.Any(user => user.Email == emailToCheck);
        }
    }
}
