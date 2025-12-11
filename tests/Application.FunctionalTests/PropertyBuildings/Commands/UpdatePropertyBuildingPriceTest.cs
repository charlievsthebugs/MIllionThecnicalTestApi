using MillionTest.Application.Common.Exceptions;
using MillionTest.Application.FunctionalTests.Builders;
using MillionTest.Application.PropertyBuildings.Commands.CreatePropertyBuilding;
using MillionTest.Application.PropertyBuildings.Commands.UpdatePropertyBuildingPrice;
using MillionTest.Domain.Entities;

namespace MillionTest.Application.FunctionalTests.PropertyBuildings.Commands;

using static Testing;
public class UpdatePropertyBuildingPriceTest : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new UpdatePropertyBuildingPrice(0,0);

        await Should.ThrowAsync<ValidationException>(() => SendAsync(command));
    }

    [Test]
    public async Task ShouldUpdatePropertyBuildingPrice()
    {
        var userId =  await RunAsDefaultUserAsync();

        var owner = await new OwnerTestBuilder().BuildAsync();

        var createCommand = await SendAsync(new CreatePropertyBuilding
        {
            Address = "456 Elm St",
            Name = "Building B",
            Price = 300000m,
            YearBuilt = new DateOnly(2010, 5, 15),
            IdOwner = owner.Id
        });

        var propertyBuildingId = createCommand.Content;
        decimal newPrice = 350000m;

        var updateCommand = new UpdatePropertyBuildingPrice(propertyBuildingId, newPrice);
        var result = await SendAsync(updateCommand);
        result.Succeeded.ShouldBeTrue();


        var item = await FindAsync<PropertyBuilding>(propertyBuildingId);
        item.ShouldNotBeNull();
        item!.Price.ShouldBe(newPrice);
        item.LastModifiedBy.ShouldBe(userId);
    }

    [Test]
    public async Task ShouldRequierePropertyBuilding()
    {
        var command = new UpdatePropertyBuildingPrice(9999, 400000m);
        var result = await SendAsync(command);
        result.Succeeded.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }

    [Test]
    public async Task ShouldRequiereValidPrice()
    {
        var createCommand = await SendAsync(new CreatePropertyBuilding
        {
            Address = "789 Oak St",
            Name = "Building C",
            Price = 400000m,
            YearBuilt = new DateOnly(2015, 8, 20),
            IdOwner = 1
        });
        var propertyBuildingId = createCommand.Content;
        var command = new UpdatePropertyBuildingPrice(propertyBuildingId, -50000m);
        await Should.ThrowAsync<ValidationException>(() => SendAsync(command));
    }

    [Test]
    public async Task ShouldNotUpdatePriceIfSameValue()
    {

        var owner = await new OwnerTestBuilder().BuildAsync();

        var createCommand = await SendAsync(new CreatePropertyBuilding
        {
            Address = "101 Pine St",
            Name = "Building D",
            Price = 450000m,
            YearBuilt = new DateOnly(2020, 3, 10),
            IdOwner = owner.Id
        });
        var propertyBuildingId = createCommand.Content;
        decimal samePrice = 450000m;
        var command = new UpdatePropertyBuildingPrice(propertyBuildingId, samePrice);
        var result = await SendAsync(command);
      
        result.Succeeded.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }
}
