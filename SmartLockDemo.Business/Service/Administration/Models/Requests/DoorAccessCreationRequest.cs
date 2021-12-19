namespace SmartLockDemo.Business.Service.Administration
{
    /// <summary>
    /// Contains door access creation parameters
    /// </summary>
    public class DoorAccessCreationRequest
    {
        /// <summary>
        /// Id of the door that will be accessible by the tag
        /// </summary>
        public int DoorId { get; set; }
        /// <summary>
        /// Id of the tag that will have access to the door
        /// </summary>
        public int TagId { get; set; }
    }
}
