using MillionTest.Application.Common.Interfaces;
using MillionTest.Application.Common.Models;
using MillionTest.Domain.Events;

namespace MillionTest.Application.PropertyBuildings.Commands.UpdatePropertyBuilding;

public record UpdatePropertyBuilding : IRequest<Result<bool>>
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public DateOnly YearBuilt { get; init; }
}

public class UpdatePropertyBuildingCommandHandler : IRequestHandler<UpdatePropertyBuilding, Result<bool>>
{
    private readonly IApplicationDbContext _context;
    public UpdatePropertyBuildingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<bool>> Handle(UpdatePropertyBuilding request, CancellationToken cancellationToken)
    {
        try
        {
            var propertyBuilding = await _context.PropertyBuildings
                    .FindAsync(request.Id, cancellationToken);

            if (propertyBuilding == null)
                return Result<bool>.Failure(["Property building not found."]);

            propertyBuilding.Name = request.Name;
            propertyBuilding.Address = request.Address;
            propertyBuilding.YearBuilt = request.YearBuilt;


            propertyBuilding.AddDomainEvent(new PropertyBuildingUpdatedEvent(propertyBuilding));

            await _context.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure([ex.GetBaseException().Message]);
        }
    }
}
