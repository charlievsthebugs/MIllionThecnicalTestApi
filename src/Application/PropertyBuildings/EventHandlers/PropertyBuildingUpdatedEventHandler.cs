using MillionTest.Domain.Events;

namespace MillionTest.Application.PropertyBuildings.EventHandlers;

public class PropertyBuildingUpdatedEventHandler : INotificationHandler<PropertyBuildingUpdatedEvent>
{
    public Task Handle(PropertyBuildingUpdatedEvent notification, CancellationToken cancellationToken)
    {
        // Here you can implement any logic that should occur when a PropertyBuilding is updated.
        // For example, logging, sending notifications, updating related entities, etc.

        // Example: Add PropertyBuilding Trace
        return Task.CompletedTask;
    }
}
