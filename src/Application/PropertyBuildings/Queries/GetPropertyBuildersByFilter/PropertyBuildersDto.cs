using MillionTest.Domain.Entities;

namespace MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter;

public record PropertyBuildersDto(string Name, string Address, decimal Price, DateOnly YearBuilt)
{
    public string[] Gallery { get; set; } = [];

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<PropertyBuilding, PropertyBuildersDto>()
                .ForMember(dest => dest.Gallery, opt => opt.MapFrom(src =>
                    src.Images
                        .Where(p => p.Enabled)
                        .Select(img => $"/api/PropertyBuildings/{img.Id}/image").ToArray()
                ));
        }
    }
}
