using Microsoft.AspNetCore.Http;
using MillionTest.Application.Common.Exceptions;
using MillionTest.Application.FunctionalTests.Builders;
using MillionTest.Application.PropertyBuildings.Commands.AddPropertyBuildingImage;
using MillionTest.Domain.Entities;

namespace MillionTest.Application.FunctionalTests.PropertyBuildings.Commands;

using static Testing;
public class AddPropertyBuildingImageTest : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new AddPropertyBuildingImage()
        {
            IdPropertyBuilding = 0,
            Image = null!
        };

        await Should.ThrowAsync<ValidationException>(() => SendAsync(command));
    }


    [Test]
    public async Task ShouldFailWhenFileTooLarge()
    {
        var bigFile = CreateFakeFile(
            3 * 1024 * 1024,      // 3 MB
            "image/png",
            "test.png"
        );

        var command = new AddPropertyBuildingImage
        {
            IdPropertyBuilding = 1,
            Image = bigFile
        };

        var ex = await Should.ThrowAsync<ValidationException>(
            async () => await SendAsync(command)
        );

        ex.Errors.ContainsKey("Image.Length").ShouldBeTrue();
        ex.Errors["Image.Length"].First().ShouldContain("Image size cannot exceed");
    }


    [Test]
    public async Task ShouldFailWhenFileTypeNotAllowed()
    {
        var bigFile = CreateFakeFile(
            200 * 102,
             "text/plain",
             "test.txt"
        );

        var command = new AddPropertyBuildingImage
        {
            IdPropertyBuilding = 1,
            Image = bigFile
        };

        var ex = await Should.ThrowAsync<ValidationException>(
            async () => await SendAsync(command)
        );

        ex.Errors.ContainsKey("Image").ShouldBeTrue();
        ex.Errors["Image"].First().ShouldContain("Only PNG, JPG and JPEG images are allowed.");
    }


    [Test]
    public async Task ShouldFailWhenPropertyNotExist()
    {
        var file = CreateFakeFile(
           200 * 102,
           "image/png",
           "test.png"
        );

        var command = new AddPropertyBuildingImage
        {
            IdPropertyBuilding = 999,
            Image = file
        };

        var result = await SendAsync(command);

        result.Succeeded.ShouldBeFalse();
        result.Errors.Contains("Property building not valid.");
       
    }


    [Test]
    public async Task ShouldSaveWhenFileIsValid()
    {
        await RunAsDefaultUserAsync();

        var file = CreateFakeFile(
            200 * 102,
            "image/png",
            "test.png"
        );


        var propertyBuilding = await new PropertyTestBuilder()
            .WithNewOwner()
            .BuildAsync();


        var command = new AddPropertyBuildingImage
        {
            IdPropertyBuilding = propertyBuilding.Id,
            Image = file
        };

        var result = await SendAsync(command);

        result.Succeeded.ShouldBeTrue();

        var entity = await FindAsync<PropertyBuildingImage>(result.Content);

        entity.ShouldNotBeNull();
        entity.ContentType.ShouldBe("image/png");
        entity.File.ShouldNotBeNull();
    }


    private static IFormFile CreateFakeFile(long sizeBytes, string contentType, string fileName)
    {
        var content = new byte[sizeBytes];
        new Random().NextBytes(content);

        var stream = new MemoryStream(content);

        return new FormFile(stream, 0, sizeBytes, "file", fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = contentType
        };
    }
}
