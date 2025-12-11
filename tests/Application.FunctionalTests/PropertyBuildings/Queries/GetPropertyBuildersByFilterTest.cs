namespace MillionTest.Application.FunctionalTests.PropertyBuildings.Queries;

using MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter;
using static Testing;
public class GetPropertyBuildersByFilterTest : BaseTestFixture
{

    [Test]
    public async Task ShouldGetAllPropertyBuilders()
    {
        await CreateFakeData();
        var query = new GetPropertyBuildersByFilter();
        var result = await SendAsync(query);

        result.Succeeded.ShouldBeTrue();
        result.Content.TotalCount.ShouldBe(6);
        result.Content.Items.ShouldNotBeNull();
    }


    [Test]
    public async Task ShouldGetPropertyBuildersByInternalCodeFilter()
    {
        await CreateFakeData();
        var query = new GetPropertyBuildersByFilter() { InternalCode = $"PB000000{new Random().Next(1, 7)}" };
        var result = await SendAsync(query);

        result.Succeeded.ShouldBeTrue();
        result.Content.TotalCount.ShouldBe(1);
        result.Content.Items.ShouldNotBeNull();
    }


    [Test]
    public async Task ShouldGetPropertyBuildersByNameFilter() { 
        
        await CreateFakeData();
        var query = new GetPropertyBuildersByFilter
        {
            Name = "Property Test One"
        };
        var result = await SendAsync(query);
        
        result.Succeeded.ShouldBeTrue();
        result.Content.TotalCount.ShouldBe(1);
        result.Content.Items.ShouldNotBeNull();
        result.Content.Items.First().Name.ShouldBe("Property Test One");
    }

    [Test]
    public async Task ShouldGetPropertyBuildersByAddressFilter() { 
        await CreateFakeData();
        
        var query = new GetPropertyBuildersByFilter
        {
            Address = "Street 1 2P 01"
        };
        var result = await SendAsync(query);
        result.Succeeded.ShouldBeTrue();
        result.Content.TotalCount.ShouldBe(1);
        result.Content.Items.ShouldNotBeNull();
        result.Content.Items.First().Address.ShouldBe("Street 1 2P 01");
    }


    [Test]
    public async Task ShouldGetPropertyBuildersByYearBuiltFilter()
    {
        await CreateFakeData();
        var query = new GetPropertyBuildersByFilter
        {
            YearBuilt = new DateOnly(2002, 1, 1)
        };
        var result = await SendAsync(query);
        result.Succeeded.ShouldBeTrue();
        result.Content.TotalCount.ShouldBe(1);
        result.Content.Items.ShouldNotBeNull();
        result.Content.Items.First().YearBuilt.ShouldBe(new DateOnly(2002, 1, 1));
    }


    [Test]
    public async Task ShouldGetPropertyBuildersByPriceRangeFilter()
    {
        await CreateFakeData();
        var query = new GetPropertyBuildersByFilter
        {
            MinPrice = 40000,
            MaxPrice = 100000
        };
        var result = await SendAsync(query);
        result.Succeeded.ShouldBeTrue();
        result.Content.TotalCount.ShouldBe(1);
        result.Content.Items.ShouldNotBeNull();
        result.Content.Items.First().Price.ShouldBe(50000);
    }


    [Test]
    public async Task ShouldGetPropertyBuildersByAnotherPriceRangeFilter()
    {
        await CreateFakeData();
        var query = new GetPropertyBuildersByFilter
        {
            MinPrice = 900000,
            MaxPrice = 1000000
        };
        var result = await SendAsync(query);
        result.Succeeded.ShouldBeTrue();
        result.Content.TotalCount.ShouldBe(1);
        result.Content.Items.ShouldNotBeNull();
        result.Content.Items.First().Price.ShouldBe(950000);
    }


    //TODO: More tests can be added for combinations of filters
    private static async Task CreateFakeData()
    {
        //testName
        var propertyBuilding = await new Builders.PropertyTestBuilder()
            .WithNewOwner()
            .WithName("Property Test One")
            .BuildAsync();


        await new Builders.PropertyTestBuilder()
           .WithOwner(propertyBuilding.IdOwner)
           .WithAddress("Street 1 2P 01")
           .BuildAsync();


        await new Builders.PropertyTestBuilder()
           .WithOwner(propertyBuilding.IdOwner)
            .WithYearBuilt(new DateOnly(2002, 1, 1))
            .BuildAsync();


        await new Builders.PropertyTestBuilder()

            .WithOwner(propertyBuilding.IdOwner)
            .WithPrice(50000)
            .BuildAsync();


        await new Builders.PropertyTestBuilder()
            .WithOwner(propertyBuilding.IdOwner)
            .WithPrice(950000)
            .BuildAsync();


        await new Builders.PropertyTestBuilder()
            .WithNewOwner()
            .BuildAsync();
    }

}
