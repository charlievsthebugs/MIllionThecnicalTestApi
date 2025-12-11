using MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Interfaces;

namespace MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Filters;

public class PBFilterByPrice : BaseFilter
{
    public override IPropertyBuildingFilter ConfigureFilter()
    {
        if (_request.MinPrice.HasValue)
            _query = _query.Where(pb => pb.Price >= _request.MinPrice.Value);

        if (_request.MaxPrice.HasValue)
            _query = _query.Where(pb => pb.Price <= _request.MaxPrice.Value);

        return this;
    }
}
