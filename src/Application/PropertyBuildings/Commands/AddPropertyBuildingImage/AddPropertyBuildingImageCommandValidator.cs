

using Microsoft.AspNetCore.Http;

namespace MillionTest.Application.PropertyBuildings.Commands.AddPropertyBuildingImage;

public class AddPropertyBuildingImageCommandValidator : AbstractValidator<AddPropertyBuildingImage>
{

    private const long MaxFileSize = 2 * 1024 * 1024; // 2 MB

    public AddPropertyBuildingImageCommandValidator()
    {
        RuleFor(p => p.IdPropertyBuilding)
            .GreaterThan(0)
            .WithMessage("Property Building ID must be a positive integer.");

        RuleFor(x => x.Image)
           .NotNull()
           .WithMessage("Image is required.");


        RuleFor(x => x.Image.Length)
           .LessThanOrEqualTo(MaxFileSize)
           .WithMessage($"Image size cannot exceed {MaxFileSize} bytes.")
           .When(x => x.Image != null);

        RuleFor(x => x.Image)
            .Must(f => IsValidImage(f))
            .WithMessage("Only PNG, JPG and JPEG images are allowed.");
    }



    private static bool IsValidImage(IFormFile file)
    {
        if (file is null) return false;

        var allowed = new[] { "image/png", "image/jpeg", "image/jpg" };

        return allowed.Contains(file.ContentType);
    }
}

