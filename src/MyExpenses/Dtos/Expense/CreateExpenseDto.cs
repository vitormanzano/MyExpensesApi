namespace MyExpenses.Dtos.Expense
{
    public record CreateExpenseDto(
        decimal Value,
        string Description,
        DateOnly Date,
        Guid CategoryId);
    
}
