using MillionTest.Application.Common.Interfaces;
using MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Filters;
using MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Interfaces;

namespace MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Generator;

public class AllFiltersGenerator(IApplicationDbContext context, GetPropertyBuildersByFilter request, IUser user) 
    : BaseGenerator(context, request)
{
    private readonly IUser _user = user;

    public override IBaseGenerator AddFilters()
    {
        Filters = [
            new PBFilterByAddress(),
            new PBFilterByInternalCode(),
            new PBFilterByPrice(),
            new PBFilterByYearBuilt(),
            new PBFilterByName(),
        ];

        if (_user.IsAuthenticated)
            Filters.Add(new PBFilterByOwner());

        return this;
    }
}
