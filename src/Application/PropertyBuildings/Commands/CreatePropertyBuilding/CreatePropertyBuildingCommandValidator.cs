using System;
using System.Collections.Generic;
using System.Text;

namespace MillionTest.Application.PropertyBuildings.Commands.CreatePropertyBuilding;

public class CreatePropertyBuildingCommandValidator : AbstractValidator<CreatePropertyBuilding>
{
    public CreatePropertyBuildingCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(v => v.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(500).WithMessage("Address must not exceed 500 characters.");

        RuleFor(v => v.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(v => v.YearBuilt)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("Year Built cannot be in the future.");

        RuleFor(v => v.IdOwner)
            .GreaterThan(0)
            .WithMessage("Owner ID must be a positive integer.");
    }
}
