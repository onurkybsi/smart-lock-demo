using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartLockDemo.Data.Entities;

namespace SmartLockDemo.Data.Configurations
{
    internal class TagDoorConfiguration : IEntityTypeConfiguration<TagDoor>
    {
        public void Configure(EntityTypeBuilder<TagDoor> builder)
        {
            builder.ToTable("TagDoor");

            builder.HasIndex(e => new { e.TagId, e.DoorId }, "IX_N_TagId_DoorId")
                .IsUnique();

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.HasOne(d => d.Door)
                .WithOne(p => p.TagDoor)
                .HasForeignKey<TagDoor>(d => d.DoorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TagDoor_DoorId");

            builder.HasOne(d => d.Tag)
                .WithMany(p => p.TagDoors)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TagDoor_TagId");
        }
    }
}
