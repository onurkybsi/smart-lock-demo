namespace SmartLockDemo.Business.Service.User
{
    /// <summary>
    /// Contains user login parameters
    /// </summary>
    public class LogInRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
