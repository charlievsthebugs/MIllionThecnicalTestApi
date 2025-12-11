using MillionTest.Application.Common.Interfaces;
using MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Interfaces;
using MillionTest.Domain.Entities;


namespace MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Generator;


public abstract class BaseGenerator : IBaseGenerator
{
    public IQueryable<PropertyBuilding> Query = null!;
    private readonly GetPropertyBuildersByFilter _request = null!;
    public List<IPropertyBuildingFilter> Filters = [];
    private readonly IApplicationDbContext _context;


    public BaseGenerator(IApplicationDbContext context, GetPropertyBuildersByFilter request)
    {
        _context = context;
        _request = request;
    }

    public virtual IBaseGenerator InitQuery()
    {
        Query = _context.PropertyBuildings;

        return this;
    }

    public abstract IBaseGenerator AddFilters();

    public IBaseGenerator ApplyFilters()
    {
        foreach (var filter in Filters)
        {
            Query = filter
                .Init(Query, _request)
                .ConfigureFilter()
                .GetQueryable();
        }

        return this;
    }

    public IQueryable<PropertyBuilding> GetQuery() => Query;

}
