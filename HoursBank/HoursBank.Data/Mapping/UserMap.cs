using HoursBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoursBank.Data.Mapping
{
    class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Email).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Name).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(50);
            builder.Property(u => u.ClientId).HasMaxLength(50);
            builder.Property(u => u.ClientSecret).HasMaxLength(50);
            builder.Property(u => u.Hours).HasDefaultValue(0);
            builder.Property(u => u.Admin).HasDefaultValue(false);
            builder.Property(u => u.Active);
            builder.Property(u => u.TeamId);
        }
    }
}
