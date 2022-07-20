using HoursBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoursBank.Data.Mapping
{
    class BankMap : IEntityTypeConfiguration<BankEntity>
    {
        public void Configure(EntityTypeBuilder<BankEntity> builder)
        {
            builder.ToTable("Bank");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.HasIndex(b => b.Description);
            builder.Property(b => b.Approved).HasDefaultValue(false);
            builder.Property(b => b.DateApproved);
            builder.Property(b => b.Start);
            builder.Property(b => b.End);
            builder.Property(b => b.CreatedAt);
            builder.Property(b => b.UserId).IsRequired();
            builder.Property(b => b.TypeId).IsRequired();
        }
    }
}
