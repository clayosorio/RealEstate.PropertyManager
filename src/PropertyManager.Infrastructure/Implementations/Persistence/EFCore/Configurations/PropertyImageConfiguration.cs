using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyManager.Domain.PropertyImages.Entities;

namespace PropertyManager.Infrastructure.Implementations.Persistence.EFCore.Configurations
{
    public class PropertyImageConfiguration : IEntityTypeConfiguration<PropertyImage>
    {
        public void Configure(EntityTypeBuilder<PropertyImage> builder)
        {
            builder.ToTable("PropertyImage");

            builder.HasKey(pi => pi.IdPropertyImage);

            builder.Property(pi => pi.ImageUrl)
                .IsRequired();

            builder.Property(pi => pi.Enabled)
                .IsRequired();

            builder.HasOne(pi => pi.Property)
                .WithMany(p => p.PropertyImages)
                .HasForeignKey(pi => pi.IdProperty);
        }
    }
}
