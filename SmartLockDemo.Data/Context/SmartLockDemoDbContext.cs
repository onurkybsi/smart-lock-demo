using KybInfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SmartLockDemo.Data.Configurations;
using SmartLockDemo.Data.Entites;

namespace SmartLockDemo.Data
{
    internal partial class SmartLockDemoDbContext : DbContext, IDatabaseContext
    {
        public SmartLockDemoDbContext() { }

        public SmartLockDemoDbContext(DbContextOptions<SmartLockDemoDbContext> options)
            : base(options) { }

        public virtual DbSet<Door> Doors { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TagDoor> TagDoors { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserTag> UserTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SmartLockDemo;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new UserTagConfiguration());
            modelBuilder.ApplyConfiguration(new DoorConfiguration());
            modelBuilder.ApplyConfiguration(new TagDoorConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public bool AreThereAnyChanges()
            => ((DbContext)this).AreThereAnyChanges();

        public void Rollback()
            => ((DbContext)this).Rollback();
    }
}
