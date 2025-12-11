using MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Interfaces;

namespace MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Filters;

public class PBFilterByInternalCode : BaseFilter
{
    public override IPropertyBuildingFilter ConfigureFilter()
    {
        if (string.IsNullOrEmpty(_request.InternalCode))
            return this;

        _query = _query.Where(pb => pb.InternalCode == _request.InternalCode);

        return this;
    }
}
