namespace MyExpenses.Dtos.Expense
{
    public record CreateExpenseDto(
        decimal Value,
        DateOnly Date,
        Guid CategoryId);
    
}
