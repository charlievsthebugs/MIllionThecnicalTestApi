using MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Interfaces;

namespace MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Filters;

public class PBFilterByOwner : BaseFilter
{
    public override IPropertyBuildingFilter ConfigureFilter()
    {
        if (!_request.IdOwner.HasValue)
            return this;

        _query = _query.Where(pb => pb.IdOwner ==  _request.IdOwner);

        return this;
    }
}
