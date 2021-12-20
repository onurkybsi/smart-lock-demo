using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLockDemo.Data.Repositories
{
    public interface IUserDoorAccessRepository
    {
        bool CheckThatUserHasAccessTheDoor(int userId, int doorId);
    }
}
