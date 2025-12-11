namespace MillionTest.Domain.Events;

public class PropertyBuildingUpdatedEvent(PropertyBuilding item) : BaseEvent
{
    public PropertyBuilding Item { get; } = item;
}
