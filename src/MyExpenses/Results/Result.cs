using MyExpenses.Errors;

namespace MyExpenses.Results;

public sealed class Result<T>
{
    public T? Value { get; }
    public Error? Error { get; }
    public bool isSuccess => Error is null;
    public bool isFailure => !isSuccess;
    
    private Result(T value) { Value = value; }
    private Result(Error error) { Error = error; }

    public static Result<T> Ok (T value) => new(value); 
    public static Result<T> Fail (Error error) => new(error);

    // Allows return user.MapToDto()
    public static implicit operator Result<T>(T value) => Ok(value);
    // Allows return usersErrors.NotFound;
    public static implicit operator Result<T>(Error error) => Fail(error);

    public TOut Match<TOut>(Func<T, TOut> onSuccess, Func<Error, TOut> onFailure)
        => isSuccess ? onSuccess(Value!) : onFailure(Error!);
}

public sealed class Result
{
    public Error? Error { get; }
    public bool IsSuccess => Error is null;
    public bool isFailure => !IsSuccess;

    private Result() { }
    private Result(Error error) { Error = error; }

    public static readonly Result Ok = new();
    public static Result Fail(Error error) => new(error);

    public static implicit operator Result(Error error) => Fail(error);

    public TOut Match<TOut>(Func<TOut> onSuccess, Func<Error, TOut> onFailure) 
        => IsSuccess ? onSuccess() : onFailure(Error!);
}
