namespace SmartLockDemo.Data.Entities
{
    public partial class UserTag
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual User User { get; set; }
    }
}
