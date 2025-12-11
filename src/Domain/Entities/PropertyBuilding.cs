namespace MillionTest.Domain.Entities;

public class PropertyBuilding : BaseAuditableEntity
{
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required decimal Price { get; set; }
    public string? InternalCode { get; set; }
    public required DateOnly YearBuilt { get; set; }
    public required int IdOwner { get; set; }
    public Owner? Owner { get; set; }
    public ICollection<PropertyBuildingImage> Images { get; set; } = null!;
    public ICollection<PropertyBuildingTrace> Traces { get; set; } = null!;
}
