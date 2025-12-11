using Microsoft.AspNetCore.Http;
using MillionTest.Application.Common.Interfaces;
using MillionTest.Application.Common.Models;
using MillionTest.Domain.Entities;

namespace MillionTest.Application.PropertyBuildings.Commands.AddPropertyBuildingImage;

public record AddPropertyBuildingImage : IRequest<Result<int>>
{
    public required int IdPropertyBuilding { get; init; }
    public required IFormFile Image { get; init; }
}


public class AddPropertyBuildingImageCommandHandler : IRequestHandler<AddPropertyBuildingImage, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public AddPropertyBuildingImageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<int>> Handle(AddPropertyBuildingImage request, CancellationToken cancellationToken)
    {
        try
        {
            //TODO : This should be a service injected as a dependency, so that if the implementation changes,
            //the same logic is maintained. In addition, it could be used in different places.
            //BUT FOR NOW, WE WILL KEEP IT SIMPLE AND DIRECTLY USE IT HERE.

            var propertyBuilding = await _context.PropertyBuildings
                    .FindAsync(request.IdPropertyBuilding, cancellationToken);


            if (propertyBuilding == null)
                return Result<int>.Failure(["Property building not valid."]);


            using var ms = new MemoryStream();
            await request.Image.CopyToAsync(ms, cancellationToken);
            byte[] photoBytes = ms.ToArray();

            var propertyBuildingImage = new PropertyBuildingImage
            {
                PropertyBuildingId = request.IdPropertyBuilding,
                File = photoBytes,
                Enabled = true,
                PropertyBuilding = propertyBuilding,
                ContentType = request.Image.ContentType
            };

            _context.PropertyBuildingImages.Add(propertyBuildingImage);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(propertyBuildingImage.Id);

        }
        catch (Exception ex)
        {
            return Result<int>.Failure([ex.GetBaseException().Message]);
        }
    }
}
