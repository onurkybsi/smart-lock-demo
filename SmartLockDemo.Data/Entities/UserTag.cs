namespace SmartLockDemo.Data.Entities
{
    /// <summary>
    /// Represents database entity of UserTag model
    /// </summary>
    public partial class UserTag
    {
        public int UserId { get; set; }
        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual User User { get; set; }
    }
}
