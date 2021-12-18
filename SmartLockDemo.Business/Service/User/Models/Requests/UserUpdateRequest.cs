namespace SmartLockDemo.Business.Service.User
{
    /// <summary>
    /// Contains user update operation parameters
    /// </summary>
    public class UserUpdateRequest
    {
        /// <summary>
        /// User id to update
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// New value of email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// New value of password
        /// </summary>
        public string Password { get; set; }
    }
}
