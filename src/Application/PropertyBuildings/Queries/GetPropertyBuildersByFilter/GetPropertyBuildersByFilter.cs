using MillionTest.Application.Common.Interfaces;
using MillionTest.Application.Common.Mappings;
using MillionTest.Application.Common.Models;
using MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Generator;

namespace MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter;

public record GetPropertyBuildersByFilter : IRequest<Result<PaginatedList<PropertyBuildersDto>>>
{
    public string? InternalCode { get; init; }
    public string? Name { get; init; }
    public string? Address { get; init; }
    public int? IdOwner { get; init; }
    public DateOnly? YearBuilt { get; set; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}


public record GetPropertyBuildersCommandHandler : IRequestHandler<GetPropertyBuildersByFilter, Result<PaginatedList<PropertyBuildersDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;
    private readonly IMapper _mapper;

    public GetPropertyBuildersCommandHandler(IApplicationDbContext context, IUser user, IMapper mapper)
    {
        _context = context;
        _user = user;
        _mapper = mapper;
    }
    public async Task<Result<PaginatedList<PropertyBuildersDto>>> Handle(GetPropertyBuildersByFilter request, CancellationToken cancellationToken)
    {

        try
        {
            var query = new AllFiltersGenerator(_context, request, _user)
                .InitQuery()
                .AddFilters()
                .ApplyFilters()
                .GetQuery();

            var builders = await query.ProjectTo<PropertyBuildersDto>(_mapper.ConfigurationProvider)
               .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);

            return Result<PaginatedList<PropertyBuildersDto>>.Success(builders);

        }
        catch (Exception ex)
        {
            return Result<PaginatedList<PropertyBuildersDto>>.Failure([ex.Message]);
        }
    }
}
