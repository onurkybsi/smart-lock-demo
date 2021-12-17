namespace SmartLockDemo.Data.Entities
{
    /// <summary>
    /// Represents database entity of User model
    /// </summary>
    public partial class User
    {
        public int Id { get; private set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public byte Role { get; set; }
        public string AuthorizationToken { get; set; }
    }
}
