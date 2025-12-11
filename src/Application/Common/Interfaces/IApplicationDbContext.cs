using MillionTest.Domain.Entities;

namespace MillionTest.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Owner> Owners { get; }
    DbSet<PropertyBuilding> PropertyBuildings { get; }
    DbSet<PropertyBuildingImage> PropertyBuildingImages { get; }
    DbSet<PropertyBuildingTrace> PropertyBuildingTraces { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
