using MillionTest.Domain.Events;

namespace MillionTest.Application.PropertyBuildings.EventHandlers;

public class PropertyBuildingCreatedEventHandler : INotificationHandler<PropertyBuildingCreatedEvent>
{
    public Task Handle(PropertyBuildingCreatedEvent notification, CancellationToken cancellationToken)
    {
        //TODO: Implement Trace.
        return Task.CompletedTask;
    }
}
