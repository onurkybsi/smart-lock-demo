namespace SmartLockDemo.Business.Service.User
{
    /// <summary>
    /// Contains user update operation base parameters
    /// </summary>
    public class UserUpdateBaseRequest
    {
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
