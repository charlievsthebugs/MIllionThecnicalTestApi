using MillionTest.Application.Common.Interfaces;
using MillionTest.Application.Common.Models;

namespace MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildingImage;

public record GetPropertyBuildingImage : IRequest<Result<PropertyBuildingImageDto>>
{
    public int FileId { get; init; }
}


public class GetPropertyBuildingImageHandler(IApplicationDbContext context) : IRequestHandler<GetPropertyBuildingImage, Result<PropertyBuildingImageDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<PropertyBuildingImageDto>> Handle(GetPropertyBuildingImage request, CancellationToken cancellationToken)
    {
        var propertyBuildingImage = await _context.PropertyBuildingImages.FindAsync(request.FileId, cancellationToken);

        return propertyBuildingImage != null && propertyBuildingImage.Enabled
            ? Result<PropertyBuildingImageDto>.Success(new() { File = propertyBuildingImage.File, ContentType = propertyBuildingImage.ContentType })
            : Result<PropertyBuildingImageDto>.Failure(["File not found."]);
    }
}
