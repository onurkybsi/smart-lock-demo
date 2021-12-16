namespace SmartLockDemo.Business.Service.User
{
    /// <summary>
    /// Represents context data of door access control process
    /// </summary>
    public class DoorAccessControlRequest
    {
        public int DoorId { get; set; }
        public int UserId { get; set; }
    }
}
