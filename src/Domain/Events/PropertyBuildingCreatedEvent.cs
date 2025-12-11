namespace MillionTest.Domain.Events;

public class PropertyBuildingCreatedEvent(PropertyBuilding item) : BaseEvent
{
    public PropertyBuilding Item { get; } = item;
}
