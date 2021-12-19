namespace SmartLockDemo.Infrastructure.Utilities
{
    /// <summary>
    /// Contains bearer token creation parameters
    /// </summary>
    public class BearerTokenCreationRequest
    {
        /// <summary>
        /// ID of the user will be owner of created token
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Email of the user will be owner of created token
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Role of the user will be owner of created token
        /// </summary>
        public string Role { get; set; }
    }
}
