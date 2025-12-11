using Microsoft.EntityFrameworkCore;

namespace MillionTest.Infrastructure.Data.Configurations;

internal class PropertyBuildingInternalCodeSecuenceConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("PropertyBuildingInternalCodeSecuence", "dbo")
            .StartsAt(1)
            .IncrementsBy(1);
    }
}
