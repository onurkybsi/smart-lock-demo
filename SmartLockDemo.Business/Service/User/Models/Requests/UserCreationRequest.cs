namespace SmartLockDemo.Business.Service.User
{
    /// <summary>
    /// Represents the parameters that is required when creating an user
    /// </summary>
    public class UserCreationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
