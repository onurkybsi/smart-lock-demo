using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartLockDemo.Data.Entites;

namespace SmartLockDemo.Data.Configurations
{
    internal class TagDoorConfiguration : IEntityTypeConfiguration<TagDoor>
    {
        public void Configure(EntityTypeBuilder<TagDoor> builder)
        {
            builder.HasNoKey();

            builder.ToTable("TagDoor");

            builder.HasOne(d => d.Door)
                .WithMany()
                .HasForeignKey(d => d.DoorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TagDoor_DoorId");

            builder.HasOne(d => d.Tag)
                .WithMany()
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TagDoor_TagId");
        }
    }
}
