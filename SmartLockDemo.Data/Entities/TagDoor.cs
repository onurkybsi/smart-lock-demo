namespace SmartLockDemo.Data.Entities
{
    /// <summary>
    /// Represents database entity of TagDoor model
    /// </summary>
    public partial class TagDoor
    {
        public int TagId { get; set; }
        public int DoorId { get; set; }

        public virtual Door Door { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
