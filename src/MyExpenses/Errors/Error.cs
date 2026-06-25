namespace MyExpenses.Errors;

public enum ErrorType { NotFound, Conflict, Validation, Unauthorized, Unexpected }

public sealed record Error (int Code, string Message, ErrorType Type)
{
   public static Error None = new(0, string.Empty, ErrorType.Unexpected);
}
