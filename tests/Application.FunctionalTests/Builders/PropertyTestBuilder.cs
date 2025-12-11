using MillionTest.Application.PropertyBuildings.Commands.CreatePropertyBuilding;
using MillionTest.Domain.Entities;

namespace MillionTest.Application.FunctionalTests.Builders;

using static Testing;

public class PropertyTestBuilder
{
    private readonly PropertyBuilding _entity = null!;

    public PropertyTestBuilder()
    {
        _entity = new PropertyBuilding
        {
            Address = "123 Main St",
            Name = "Building A",
            Price = 250000m,
            YearBuilt = new DateOnly(2000, 1, 1),
            IdOwner = 0
        };
    }

    public PropertyTestBuilder WithName(string name)
    {
        _entity.Name = name;

        return this;
    }

    public PropertyTestBuilder WithAddress(string address)
    {
        _entity.Address = address;
        return this;
    }

    public PropertyTestBuilder WithPrice(decimal price)
    {
        _entity.Price = price;
        return this;
    }

    public PropertyTestBuilder WithYearBuilt(DateOnly yearBuilt)
    {
        _entity.YearBuilt = yearBuilt;
        return this;
    }


    public PropertyTestBuilder WithOwner(int id)
    {
        _entity.IdOwner = id;

        return this;
    }

    public PropertyTestBuilder WithNewOwner()
    {
        var owner = new Owner() { Name = "Default" };

        AddAsync(owner).Wait();

        _entity.IdOwner = owner.Id;

        return this;
    }


    public async Task<PropertyBuilding> BuildAsync()
    {
        await AddAsync(_entity);
        return _entity;
    }
}
