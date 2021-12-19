namespace SmartLockDemo.Business.Service.Administration
{
    /// <summary>
    /// Contains parameters of door access removal
    /// </summary>
    public class DoorAccessRemovalRequest
    {
        /// <summary>
        /// Id of door to be inaccessible via the tag
        /// </summary>
        public int DoorId { get; set; }
        /// <summary>
        /// Tag whose access is to be removed
        /// </summary>
        public int TagId { get; set; }
    }
}
