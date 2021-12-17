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
