using MillionTest.Application.Common.Interfaces;
using MillionTest.Application.Common.Models;
using MillionTest.Domain.Entities;
using MillionTest.Domain.Events;

namespace MillionTest.Application.PropertyBuildings.Commands.CreatePropertyBuilding;

public record CreatePropertyBuilding : IRequest<Result<int>>
{
    public required string Name { get; init; }
    public required string Address { get; init; }
    public required decimal Price { get; init; }
    public required DateOnly YearBuilt { get; init; }
    public required int IdOwner { get; init; }
}


public record CreatePropertyBuildingCommandHandler : IRequestHandler<CreatePropertyBuilding, Result<int>>
{
    private readonly IApplicationDbContext _context;
    public CreatePropertyBuildingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    async Task<Result<int>> IRequestHandler<CreatePropertyBuilding, Result<int>>.Handle(CreatePropertyBuilding request, CancellationToken cancellationToken)
    {

        try
        {
            var owner = _context.Owners.Find(request.IdOwner);

            if (owner == null)
                return Result<int>.Failure(["Owner not found."]);


            var entity = new PropertyBuilding
            {
                Name = request.Name,
                Address = request.Address,
                Price = request.Price,
                YearBuilt = request.YearBuilt,
                IdOwner = request.IdOwner,
                Owner = owner
            };

            entity.AddDomainEvent(new PropertyBuildingCreatedEvent(entity));

            _context.PropertyBuildings.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(entity.Id);
        }
        catch (Exception ex)
        {
            return Result<int>.Failure([ex.Message]);
        }
    }
}

