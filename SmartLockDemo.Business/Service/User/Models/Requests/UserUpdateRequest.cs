namespace SmartLockDemo.Business.Service.User
{
    /// <summary>
    /// Contains user update operation parameters
    /// </summary>
    public class UserUpdateRequest : UserUpdateBaseRequest
    {
        /// <summary>
        /// User id to update
        /// </summary>
        public int Id { get; set; }

        public UserUpdateRequest() { }

        public UserUpdateRequest(UserUpdateBaseRequest baseRequest)
        {
            Email = baseRequest.Email;
            Password = baseRequest.Password;
        }
    }
}
