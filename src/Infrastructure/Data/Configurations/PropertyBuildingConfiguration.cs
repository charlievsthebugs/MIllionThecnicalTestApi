using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MillionTest.Domain.Entities;

namespace MillionTest.Infrastructure.Data.Configurations;

public class PropertyBuildingConfiguration : IEntityTypeConfiguration<PropertyBuilding>
{
    public void Configure(EntityTypeBuilder<PropertyBuilding> builder)
    {
        builder.ToTable("PropertyBuildings");

        builder.Property(e => e.Id)
            .UseIdentityColumn()
           .ValueGeneratedOnAdd();

        builder.Property(pb => pb.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pb => pb.Address)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(pb => pb.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        // Set default value using SQL sequence
        builder.Property(pb => pb.InternalCode)
            .IsRequired()
            .HasMaxLength(9)
            .HasDefaultValueSql("RIGHT('PB000000' + CAST(NEXT VALUE FOR dbo.PropertyBuildingInternalCodeSecuence AS VARCHAR(9)), 9)");


        // Unique constraint on InternalCode
        builder.HasIndex(pb => pb.InternalCode)
           .IsUnique();

        builder.Property(pb => pb.YearBuilt)
            .IsRequired()
            .HasColumnType("date");

        builder.HasOne(pb => pb.Owner)
             .WithMany(o => o.PropertyBuildings)
            .HasForeignKey(pb => pb.IdOwner)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(pb => pb.Images)
            .WithOne(img => img.PropertyBuilding)
            .HasForeignKey(img => img.PropertyBuildingId);

        builder.HasMany(pb => pb.Traces)
            .WithOne(trace => trace.PropertyBuilding)
            .HasForeignKey(trace => trace.PropertyBuildingId);

    }
}
