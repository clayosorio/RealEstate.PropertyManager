using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyManager.Domain.PropertyTraces.Entities;

namespace PropertyManager.Infrastructure.Implementations.Persistence.EFCore.Configurations
{
    public class PropertyTraceConfiguration : IEntityTypeConfiguration<PropertyTrace>
    {
        void IEntityTypeConfiguration<PropertyTrace>.Configure(EntityTypeBuilder<PropertyTrace> builder)
        {
            builder.ToTable("PropertyTrace");

            builder.HasKey(pt => pt.IdPropertyTrace);

            builder.Property(pt => pt.DateSale)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(pt => pt.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(pt => pt.Value)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(pt => pt.Tax)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(pt => pt.Property)
                .WithMany(p => p.PropertyTraces)
                .HasForeignKey(pt => pt.IdProperty);
        }
    }
}
