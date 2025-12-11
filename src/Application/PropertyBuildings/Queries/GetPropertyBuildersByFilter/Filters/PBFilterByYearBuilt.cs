using MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Interfaces;

namespace MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Filters;

public class PBFilterByYearBuilt : BaseFilter
{
    public override IPropertyBuildingFilter ConfigureFilter()
    {
        if (!_request.YearBuilt.HasValue) return this;

        _query = _query.Where(pb => pb.YearBuilt == _request.YearBuilt.Value);

        return this;
    }
}
