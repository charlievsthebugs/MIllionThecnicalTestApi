using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MillionTest.Application.Common.Interfaces;
using MillionTest.Domain.Entities;
using MillionTest.Infrastructure.Data.Configurations;
using MillionTest.Infrastructure.Identity;

namespace MillionTest.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<PropertyBuilding> PropertyBuildings => Set<PropertyBuilding>();
    public DbSet<PropertyBuildingImage> PropertyBuildingImages => Set<PropertyBuildingImage>();
    public DbSet<PropertyBuildingTrace> PropertyBuildingTraces => Set<PropertyBuildingTrace>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        PropertyBuildingInternalCodeSecuenceConfiguration.Configure(builder);
    }
}
