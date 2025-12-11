namespace MillionTest.Application.FunctionalTests;

using Microsoft.EntityFrameworkCore;
using MillionTest.Application.Common.Exceptions;
using MillionTest.Application.FunctionalTests.Builders;
using MillionTest.Application.PropertyBuildings.Commands.CreatePropertyBuilding;
using MillionTest.Domain.Entities;
using Shouldly;
using static Testing;
public class CreatePropertyBuildingTest : BaseTestFixture
{

    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreatePropertyBuilding()
        {
            Address = string.Empty,
            Name = string.Empty,
            Price = 0m,
            YearBuilt = default,
            IdOwner = 0
        };

        await Should.ThrowAsync<ValidationException>(() => SendAsync(command));
    }

    [Test]
    public async Task ShouldCreatePropertyBuilding()
    {
        var userId = await RunAsDefaultUserAsync();

        string address = "123 Main St"
             , name = "Building A";

        decimal price = 250000m;

        var owner = await new OwnerTestBuilder().BuildAsync();

        var command = await SendAsync(new CreatePropertyBuilding
        {
            Address = address,
            Name = name,
            Price = price,
            YearBuilt = new DateOnly(2000, 1, 1),
            IdOwner = owner.Id
        });

        var item = await FindAsync<PropertyBuilding>(command.Content);

        item.ShouldNotBeNull();

        item!.Address.ShouldBe(address);
        item.Name.ShouldBe(name);
        item.Price.ShouldBe(price);
        item.YearBuilt.ShouldBe(new DateOnly(2000, 1, 1));
        item.InternalCode.ShouldBe("PB0000001");
        item.CreatedBy.ShouldBe(userId);
        item.Created.ShouldBe(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModifiedBy.ShouldBe(userId);
        item.LastModified.ShouldBe(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }


    [Test]
    public async Task ShouldRequiereOwner()
    {
        var command = await SendAsync(new CreatePropertyBuilding
        {
            Address = "123 Main St",
            Name = "Building A",
            Price = 250000m,
            YearBuilt = new DateOnly(2000, 1, 1),
            IdOwner = 999
        });

        command.Succeeded.ShouldBeFalse();
        command.Errors.ShouldNotBeNull().ShouldNotBeEmpty();
    }


    [Test]
    public async Task ShouldGenerateInternalCodeSequentially()
    {

        var propertyBuilding1 = await new PropertyTestBuilder()
            .WithNewOwner()
            .BuildAsync();

        var entity1 = await FindAsync<PropertyBuilding>(propertyBuilding1.Id);

        entity1.ShouldNotBeNull();
        entity1.InternalCode.ShouldBe("PB0000001");

        var propertyBuilding2 = await new PropertyTestBuilder()
            .WithNewOwner()
            .BuildAsync();

        var entity2 = await FindAsync<PropertyBuilding>(propertyBuilding2.Id);

        entity2.ShouldNotBeNull();
        entity2.InternalCode.ShouldBe("PB0000002");
    }

}
