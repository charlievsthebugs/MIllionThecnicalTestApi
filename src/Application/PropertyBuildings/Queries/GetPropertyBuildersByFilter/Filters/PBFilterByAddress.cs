using MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Interfaces;

namespace MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Filters;

public class PBFilterByAddress : BaseFilter
{
    public override IPropertyBuildingFilter ConfigureFilter()
    {
        if (string.IsNullOrEmpty(_request.Address))
            return this;

        _query = _query.Where(pb => pb.Address != null && pb.Address.Contains(_request.Address));

        return this;
    }
}
