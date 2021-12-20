namespace SmartLockDemo.WebAPI.Utilities
{
    /// <summary>
    /// Specifies REST services URIs
    /// </summary>
    internal static class RestServiceUris
    {
        /// <summary>
        /// Specifies REST services URIs that should be used by admins to administrate the system
        /// </summary>
        public static class Administration
        {
            /// <summary>
            /// URI should be called to create a new user in the system
            /// </summary>
            public const string CreateUser = "/administration/createuser";
            /// <summary>
            /// URI should be called to create a new door in the system
            /// </summary>
            public const string CreateDoor = "/administration/createdoor";
            /// <summary>
            /// URI should be called to create a new tag in the system
            /// </summary>
            public const string CreateTag = "/administration/createtag";
            /// <summary>
            /// URI should be called to create a new access to a door
            /// </summary>
            public const string CreateDoorAccess = "/administration/createdooraccess";
            /// <summary>
            /// URI should be called to tag an user
            /// </summary>
            public const string TagUser = "/administration/taguser";
            /// <summary>
            /// URI should be called to remove a door access from a tag
            /// </summary>
            public const string RemoveDoorAccess = "/administration/removedooraccess";
            /// <summary>
            /// URI should be called to remove a tag from an user
            /// </summary>
            public const string RemoveUserTag = "/administration/removeusertag";
            /// <summary>
            /// URI should be called to delete an user
            /// </summary>
            public const string DeleteUser = "/administration/deleteuser";
            /// <summary>
            /// URI should be called to delete a door
            /// </summary>
            public const string DeleteDoor = "/administration/deletedoor";
            /// <summary>
            /// URI should be called to delete a tag
            /// </summary>
            public const string DeleteTag = "/administration/deletetag";
            /// <summary>
            /// URI should be called to get all users in the system
            /// </summary>
            public const string GetAllUsers = "/administration/getallusers";
            /// <summary>
            /// URI should be called to get all door in the system
            /// </summary>
            public const string GetAllDoors = "/administration/getalldoors";
            /// <summary>
            /// URI should be called to get all tags in the system
            /// </summary>
            public const string GetAllTags = "/administration/getalltags";
        }

        /// <summary>
        /// Specifies REST services URIs that can be used to check door access
        /// </summary>
        public static class DoorAccess
        {
            /// <summary>
            /// URI checks whether user has door access or not
            /// </summary>
            public const string CheckDoorAccess = "/dooraccess";
        }

        /// <summary>
        /// Specifies REST services URIs that can be used by users
        /// </summary>
        public static class User
        {
            /// <summary>
            /// URI should be called to update an user
            /// </summary>
            public const string UpdateUser = "/user/update";
            /// <summary>
            /// URI should be called to login to the system
            /// </summary>
            public const string Login = "/user/login";
        }
    }
}
