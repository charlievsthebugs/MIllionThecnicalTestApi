using MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Interfaces;
using MillionTest.Domain.Entities;

namespace MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Filters;

public abstract class BaseFilter: IPropertyBuildingFilter
{
    protected IQueryable<PropertyBuilding> _query = null!;
    protected GetPropertyBuildersByFilter _request = null!;

    public IPropertyBuildingFilter Init(IQueryable<PropertyBuilding> query, GetPropertyBuildersByFilter request)
    {
        _query = query;
        _request = request;

        return this;
    }

    public abstract IPropertyBuildingFilter ConfigureFilter();

    public IQueryable<PropertyBuilding> GetQueryable() => _query;
}
