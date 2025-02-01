using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pottmayer.MTV.Core.Domain.Modules.Users.Entities;

namespace Pottmayer.MTV.Adapter.Data.Modules.Users.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("usr001_user");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(u => u.Name).HasColumnName("name").IsRequired();
            builder.Property(u => u.Username).HasColumnName("username").IsRequired();
            builder.Property(u => u.Email).HasColumnName("email").IsRequired();
            builder.Property(u => u.Password).HasColumnName("password").IsRequired();
            builder.Property(u => u.PasswordSalt).HasColumnName("password_salt").IsRequired();
            builder.Property(u => u.Role).HasColumnName("role").HasConversion<int>().IsRequired();

            builder.HasMany(u => u.CreatedPhrases)
                .WithOne(p => p.Creator)
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.UpdatedPhrases)
                .WithOne(p => p.Updater)
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(u => u.Username).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}
