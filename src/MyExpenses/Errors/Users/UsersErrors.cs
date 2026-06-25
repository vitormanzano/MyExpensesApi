namespace MyExpenses.Errors.Users;

public static class UsersErrors
{
    public static readonly Error EmailAlreadyExists = new(409, "Email already exists.", ErrorType.Conflict);
    public static readonly Error CpfAlreadyExists   = new(409, "CPF already exists.", ErrorType.Conflict);
    public static readonly Error NotFound           = new(409, "User not found.", ErrorType.NotFound);
    public static readonly Error SignUpFailed       = new(500, "Could not sign up.", ErrorType.Unexpected);
    public static readonly Error CredentialsWrong   = new(401, "Wrong credentials.", ErrorType.Validation);
    public static readonly Error UpdateFailed       = new(500, "Could not update.", ErrorType.Unexpected);
    public static readonly Error DeleteFailed       = new(500, "Could not delete.", ErrorType.Unexpected);
}
