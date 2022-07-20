using HoursBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoursBank.Data.Mapping
{
    class TypeMap : IEntityTypeConfiguration<TypeEntity>
    {
        public void Configure(EntityTypeBuilder<TypeEntity> builder)
        {
            builder.ToTable("Type");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.HasIndex(t => t.Description).IsUnique();
            builder.Property(t => t.Description).IsRequired();
            builder.Property(t => t.Increase).IsRequired();
        }
    }
}
