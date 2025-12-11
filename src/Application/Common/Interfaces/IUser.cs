namespace MillionTest.Application.Common.Interfaces;

public interface IUser
{
    string? Id { get; }
    List<string>? Roles { get; }

    bool IsAuthenticated { get; }

}
