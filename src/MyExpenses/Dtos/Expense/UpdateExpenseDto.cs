namespace MyExpenses.Dtos.Expense;

public record UpdateExpenseDto(
    Guid ExpenseId,
    decimal Value,
    DateOnly Date,
    Guid CategoryId);
