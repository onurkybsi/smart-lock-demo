using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartLockDemo.Data.Entities;

namespace SmartLockDemo.Data.Configurations
{
    internal class UserTagConfiguration : IEntityTypeConfiguration<UserTag>
    {
        public void Configure(EntityTypeBuilder<UserTag> builder)
        {
            builder.HasNoKey();

            builder.ToTable("UserTag");

            builder.HasOne(d => d.Tag)
                .WithMany()
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserTag_TagId");

            builder.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserTag_UserId");
        }
    }
}
