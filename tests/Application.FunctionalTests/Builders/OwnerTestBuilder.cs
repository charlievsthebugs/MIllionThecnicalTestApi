using MillionTest.Domain.Entities;

namespace MillionTest.Application.FunctionalTests.Builders;


using static Testing;

public class OwnerTestBuilder
{
    private readonly Owner _entity = null!;

    public OwnerTestBuilder()
    {
        _entity = new Owner()
        {
            Name = "Default Owner",
        };
    }

    public OwnerTestBuilder WithName(string name)
    {
        _entity.Name = name;

        return this;
    }

    public async Task<Owner> BuildAsync()
    {
        await AddAsync(_entity);
        return _entity;
    }
}
