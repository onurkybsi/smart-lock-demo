namespace SmartLockDemo.Data.Entities
{
    public partial class Door
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual TagDoor TagDoor { get; set; }
    }
}
