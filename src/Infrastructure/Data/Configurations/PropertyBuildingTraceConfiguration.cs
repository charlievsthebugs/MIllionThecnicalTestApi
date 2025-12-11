using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MillionTest.Domain.Entities;

namespace MillionTest.Infrastructure.Data.Configurations;

internal class PropertyBuildingTraceConfiguration : IEntityTypeConfiguration<PropertyBuildingTrace>
{
    public void Configure(EntityTypeBuilder<PropertyBuildingTrace> builder)
    {
        builder.ToTable("PropertyBuildingTraces");

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.DateSale)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(e => e.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.SaleValue)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(e => e.Tax)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.HasOne(d => d.PropertyBuilding)
            .WithMany(p => p.Traces)
            .HasForeignKey(d => d.PropertyBuildingId);
    }
}
