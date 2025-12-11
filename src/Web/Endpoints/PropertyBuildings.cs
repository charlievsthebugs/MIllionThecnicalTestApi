using Microsoft.AspNetCore.Mvc;
using MillionTest.Application.PropertyBuildings.Commands.AddPropertyBuildingImage;
using MillionTest.Application.PropertyBuildings.Commands.CreatePropertyBuilding;
using MillionTest.Application.PropertyBuildings.Commands.UpdatePropertyBuilding;
using MillionTest.Application.PropertyBuildings.Commands.UpdatePropertyBuildingPrice;
using MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter;
using MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildingImage;
using MillionTest.Web.RequestDtos;

namespace MillionTest.Web.Endpoints;

public class PropertyBuildings : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost(CreatePropertyBuilding).RequireAuthorization();

        groupBuilder.MapPost(AddPropertyBuildingImage, "{id}/addimage")
            .RequireAuthorization()
            .DisableAntiforgery();

        groupBuilder.MapPut("{id}",UpdatePropertyBuilding).RequireAuthorization();
        groupBuilder.MapPatch("{id}/price", UpdatePropertyBuildingPrice).RequireAuthorization();
        groupBuilder.MapGet(GetPropertyBuildingImage, "{id}/image");
        groupBuilder.MapGet(GetPropertyBuildersByFilters, "byfilters");
    }

    public async Task<IResult> CreatePropertyBuilding(
        ISender sender, 
        [FromBody] CreatePropertyBuilding command)
    {
        var result = await sender.Send(command);

        return result.Succeeded
            ? TypedResults.Created($"/PropertyBuildings/{result.Content}", result)
            : TypedResults.BadRequest(result);
    }

    public async Task<IResult> AddPropertyBuildingImage(
        ISender sender,
        [FromRoute] int id,
        [FromForm] IFormFile file)
    {
        var result = await sender.Send(new AddPropertyBuildingImage() { IdPropertyBuilding = id, Image = file });

        return result.Succeeded
            ? TypedResults.Created($"/PropertyBuildings/{result.Content}/image", result)
            : TypedResults.BadRequest(result);
    }

    public async Task<IResult> UpdatePropertyBuildingPrice(
        ISender sender,
        [FromRoute] int id,
        [FromBody] UpdatePriceRequest request)
    {
        var result = await sender.Send(new UpdatePropertyBuildingPrice(id, request.Price));

        return result.Succeeded
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }


    public async Task<IResult> UpdatePropertyBuilding(
      ISender sender,
      [FromRoute] int id,
      [FromBody] UpdatePropertyBuilding command)
    {
        command = command with { Id = id };

        var result = await sender.Send(command);

        return result.Succeeded
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }


    public async Task<IResult> GetPropertyBuildingImage(
        ISender sender,
        [FromRoute] int id)
    {
        var result = await sender.Send(new GetPropertyBuildingImage() { FileId = id });

        return result.Succeeded
            ? TypedResults.File(result.Content.File, result.Content.ContentType)
            : TypedResults.NoContent();
    }


    public async Task<IResult> GetPropertyBuildersByFilters(
       ISender sender,
       [AsParameters] GetPropertyBuildersByFilter query)
    {
        var result = await sender.Send(query);

        return !result.Succeeded 
            ? TypedResults.BadRequest(result) 
            : TypedResults.Ok(result);
    }


}
