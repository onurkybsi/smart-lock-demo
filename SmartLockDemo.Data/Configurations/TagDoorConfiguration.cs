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

            builder.HasOne(d => d.Door)
                .WithMany(p => p.TagDoors)
                .HasForeignKey(d => d.DoorId)
                .HasConstraintName("FK_TagDoor_DoorId");

            builder.HasOne(d => d.Tag)
                .WithMany(p => p.TagDoors)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("FK_TagDoor_TagId");
        }
    }
}
