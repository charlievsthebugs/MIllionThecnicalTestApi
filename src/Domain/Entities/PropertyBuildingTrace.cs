namespace MillionTest.Domain.Entities;

public class PropertyBuildingTrace : BaseAuditableEntity
{
    public required DateTime DateSale { get; set; }
    public required string Name { get; set; }
    public required decimal SaleValue { get; set; }
    public required decimal Tax { get; set; }
    public required int PropertyBuildingId { get; set; }
    public required PropertyBuilding PropertyBuilding { get; set; }
}
