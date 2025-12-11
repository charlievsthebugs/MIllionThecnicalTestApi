using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MillionTest.Domain.Entities;

namespace MillionTest.Infrastructure.Data.Configurations;

public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.ToTable("Owners");

        builder.Property(o => o.Id)
            .ValueGeneratedOnAdd();

        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(o => o.Address)
            .HasMaxLength(500);

        builder.Property(o => o.Photo)
            //to store image as binary data
            .HasColumnType("varbinary(max)"); 

        builder.Property(o => o.Birthday)
            //store only date part without time
            .HasColumnType("date"); 

        builder.HasMany(o => o.PropertyBuildings)
            .WithOne(pb => pb.Owner)
            .HasForeignKey(pb => pb.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
