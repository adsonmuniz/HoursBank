using HoursBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoursBank.Data.Mapping
{
    class CoordinatorMap : IEntityTypeConfiguration<CoordinatorEntity>
    {
        public void Configure(EntityTypeBuilder<CoordinatorEntity> builder)
        {
            builder.ToTable("Coordinator");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.TeamId).IsRequired();
            builder.Property(c => c.UserId).IsRequired();
        }
    }
}
