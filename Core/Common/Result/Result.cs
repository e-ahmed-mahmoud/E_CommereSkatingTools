namespace Core.Common.Result;

public class Result
{
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; } = Error.None;

    public Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException("Result can be success and hold errors at same time");
        }
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result<TValue> Success<TValue>(TValue value) => new(true, Error.None, value);
    public static Result<TValue> Failure<TValue>(Error error) => new(false, error, default!);
}

public class Result<TValue>(bool isSuccess, Error error, TValue value) : Result(isSuccess, error)
{
    private readonly TValue _value = value;

    public TValue Value => IsSuccess ? _value : throw new InvalidOperationException("invalid Result, value can't be defined in failure cases");
}
