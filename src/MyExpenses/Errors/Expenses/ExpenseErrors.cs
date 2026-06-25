namespace MyExpenses.Errors.Expenses;

public class ExpenseErrors
{
    public static readonly Error NotFound           = new(409, "Expense not found.", ErrorType.NotFound);
    public static readonly Error ValueLowerThanZero = new(401, "Expense value must be greater than zero!", ErrorType.Validation);
    public static readonly Error InvalidMonth       = new(401, "Month must be between 1 and 12!", ErrorType.Validation);
    public static readonly Error InvalidYear        = new(401, "Invalid year!", ErrorType.Validation);
    public static readonly Error CreateFailed       = new(500, "Could not create expense.", ErrorType.Unexpected);
    public static readonly Error UpdateFailed       = new(500, "Could not update expense.", ErrorType.Unexpected);
    public static readonly Error DeleteFailed       = new(500, "Could not delete expense.", ErrorType.Unexpected);
}
