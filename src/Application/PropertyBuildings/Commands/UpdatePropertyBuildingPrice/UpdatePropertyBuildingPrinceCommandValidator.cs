namespace MillionTest.Application.PropertyBuildings.Commands.UpdatePropertyBuildingPrice;

public class UpdatePropertyBuildingPrinceCommandValidator : AbstractValidator<UpdatePropertyBuildingPrice>
{
    public UpdatePropertyBuildingPrinceCommandValidator()
    {
        RuleFor(p => p.Id)
            .GreaterThan(0).WithMessage("Property Building ID must be a positive integer.");

        RuleFor(p => p.NewPrice)
            .GreaterThan(0).WithMessage("New price must be greater than zero.");
    }
}
