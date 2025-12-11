using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MillionTest.Domain.Entities;

namespace MillionTest.Infrastructure.Data.Configurations;

public class PropertyBuildingImageConfiguration : IEntityTypeConfiguration<PropertyBuildingImage>
{
    public void Configure(EntityTypeBuilder<PropertyBuildingImage> builder)
    {
        builder.ToTable("PropertyBuildingImages");

        builder.Property(e => e.Id)
          .ValueGeneratedOnAdd();

        builder.Property(e => e.File)
           .IsRequired()
           .HasColumnType("varbinary(max)");//to store image as binary data

        builder.Property(e => e.Enabled)
            .IsRequired();

        builder.HasOne(d => d.PropertyBuilding)
            .WithMany(p => p.Images)
            .HasForeignKey(d => d.PropertyBuildingId)
            .IsRequired();
    }
}
