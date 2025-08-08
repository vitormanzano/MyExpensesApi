namespace MyExpenses.Dtos.Expense
{
    public record ResponseExpenseDto(
        Guid Id,
        decimal Value,
        string Description,
        DateOnly Date,
        Guid CategoryId);
    
}
