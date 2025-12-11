using Microsoft.EntityFrameworkCore;
using MillionTest.Domain.Entities;

namespace MillionTest.Application.PropertyBuildings.Queries.GetPropertyBuildersByFilter.Interfaces;

public interface IBaseGenerator
{
    IBaseGenerator InitQuery();
    IBaseGenerator AddFilters();
    IBaseGenerator ApplyFilters();
    IQueryable<PropertyBuilding> GetQuery();
}
