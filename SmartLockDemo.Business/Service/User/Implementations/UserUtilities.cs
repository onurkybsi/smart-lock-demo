using System;

namespace SmartLockDemo.Business.Service.User
{
    /// <summary>
    /// Provides various utilities related with User services
    /// </summary>
    public static class UserUtilities
    {
        private static string USER_ROLE_NAME = "User";
        private static string ADMIN_ROLE_NAME = "Admin";

        /// <summary>
        /// Converts given role name to Role type equivalence
        /// </summary>
        /// <param name="roleName">Role name to receive Role type</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">It is thrown if given role name is undefined role</exception>
        public static Role ConvertToRole(string roleName)
        {
            if (roleName == USER_ROLE_NAME)
                return Role.User;
            if (roleName == ADMIN_ROLE_NAME)
                return Role.Admin;
            throw new InvalidOperationException($"There is no role definition called: {roleName}");
        }
    }
}
