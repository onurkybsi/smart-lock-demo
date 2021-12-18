using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartLockDemo.Data.Entities;

namespace SmartLockDemo.Data.Configurations
{
    internal class UserTagConfiguration : IEntityTypeConfiguration<UserTag>
    {
        public void Configure(EntityTypeBuilder<UserTag> builder)
        {
            builder.ToTable("UserTag");

            builder.HasIndex(e => new { e.UserId, e.TagId }, "IX_N_UserId_TagId")
                .IsUnique();

            builder.HasOne(d => d.Tag)
                .WithMany(p => p.UserTags)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("FK_UserTag_TagId");

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserTags)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserTag_UserId");
        }
    }
}
