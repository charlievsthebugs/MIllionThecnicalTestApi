namespace MillionTest.Application.Common.Models;

public class Result
{
    internal Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = [.. errors];
    }

    public bool Succeeded { get; init; }

    public string[] Errors { get; init; }

    public static Result Success()
    {
        return new Result(true, Array.Empty<string>());
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }
}

public class Result<T> : Result
{
    public T Content { get; }

    private Result(bool succeeded, T content, IEnumerable<string> errors)
        : base(succeeded, errors)
    {
        Content = content;
    }


    public static Result<T> Success(T content)
    {
        return new Result<T>(true, content, []);
    }

    public static new Result<T> Failure(IEnumerable<string> errors)
    {
        return new Result<T>(false, default!, errors);
    }

}
