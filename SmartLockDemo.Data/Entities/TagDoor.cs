namespace SmartLockDemo.Data.Entities
{
    public partial class TagDoor
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public int DoorId { get; set; }

        public virtual Door Door { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
