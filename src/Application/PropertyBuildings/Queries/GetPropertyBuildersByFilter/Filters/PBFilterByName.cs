using MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Interfaces;

namespace MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Filters;

internal class PBFilterByName : BaseFilter
{
    public override IPropertyBuildingFilter ConfigureFilter()
    {
        if (string.IsNullOrEmpty(_request.Name))
            return this;

        _query = _query.Where(pb => pb.Name.Contains(_request.Name));

        return this;
    }
}
