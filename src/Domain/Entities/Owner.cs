namespace MillionTest.Domain.Entities;

public class Owner : BaseAuditableEntity
{
    public required string Name { get; set; }
    public string? Address { get; set; }
    public string? Photo { get; set; }
    public DateTime? Birthday { get; set; }
    public ICollection<PropertyBuilding>? PropertyBuildings { get; set; }

}
