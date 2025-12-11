using MillionTest.Application.Common.Interfaces;
using MillionTest.Application.Common.Models;
using MillionTest.Domain.Events;

namespace MillionTest.Application.PropertyBuildings.Commands.UpdatePropertyBuildingPrice;

public record UpdatePropertyBuildingPrice(int Id, decimal NewPrice) : IRequest<Result<bool>>;

public class UpdatePropertyBuildingPriceCommandHandler : IRequestHandler<UpdatePropertyBuildingPrice, Result<bool>>
{
    private readonly IApplicationDbContext _context;

    public UpdatePropertyBuildingPriceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<bool>> Handle(UpdatePropertyBuildingPrice request, CancellationToken cancellationToken)
    {
        try
        {
            var propertyBuilding = await _context.PropertyBuildings
                .FindAsync(request.Id, cancellationToken);

            if (propertyBuilding == null)
                return Result<bool>.Failure([$"Property building not valid."]);

            if (propertyBuilding.Price == request.NewPrice)
                return Result<bool>.Failure(["The new price is the same as the current price."]);

            propertyBuilding.Price = request.NewPrice;

            propertyBuilding.AddDomainEvent(new PropertyBuildingUpdatedEvent(propertyBuilding));

            _context.PropertyBuildings.Update(propertyBuilding);

            await _context.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure([ex.GetBaseException().Message]);
        }
       
    }
}
