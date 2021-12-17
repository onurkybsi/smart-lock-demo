using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartLockDemo.Data.Entites;

namespace SmartLockDemo.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable("User");

            builder.HasIndex(e => e.Email, "IX_U_Email")
                .IsUnique();

            builder.Property(e => e.AuthorizationToken)
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.HashedPassword)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
        }
    }
}
