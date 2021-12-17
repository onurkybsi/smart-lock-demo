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
    }
}
