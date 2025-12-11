namespace MillionTest.Domain.Entities;

public class PropertyBuildingImage : BaseAuditableEntity
{
    public required byte[] File { get; set; }
    public required string ContentType { get; set; }
    public bool Enabled { get; set; }
    public required int PropertyBuildingId { get; set; }
    public required PropertyBuilding PropertyBuilding { get; set; }
}
