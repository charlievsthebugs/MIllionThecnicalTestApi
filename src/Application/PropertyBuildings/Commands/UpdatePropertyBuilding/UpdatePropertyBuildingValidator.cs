namespace MillionTest.Application.PropertyBuildings.Commands.UpdatePropertyBuilding;

public class UpdatePropertyBuildingValidator : AbstractValidator<UpdatePropertyBuilding>
{
    public UpdatePropertyBuildingValidator()
    {
        RuleFor(p => p.Id)
            .GreaterThan(0)
            .WithMessage("Property Building ID must be a positive integer.");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters.");

        RuleFor(p => p.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.");

        RuleFor(p => p.YearBuilt)
            .NotEmpty().WithMessage("Year Built is required.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Year Built cannot be in the future.");
    }
}
