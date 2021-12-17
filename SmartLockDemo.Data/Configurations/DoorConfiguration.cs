using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartLockDemo.Data.Entites;

namespace SmartLockDemo.Data.Configurations
{
    internal class DoorConfiguration : IEntityTypeConfiguration<Door>
    {
        public void Configure(EntityTypeBuilder<Door> builder)
        {
            builder.ToTable("Door");

            builder.HasIndex(e => e.Name, "IX_U_Name")
                .IsUnique();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
