using MillionTest.Domain.Entities;

namespace MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Interfaces;

public interface IPropertyBuildingFilter
{
    IPropertyBuildingFilter ConfigureFilter();
    IPropertyBuildingFilter Init(IQueryable<PropertyBuilding> query, GetPropertyBuildersByFilter request);
    IQueryable<PropertyBuilding> GetQueryable();
}
