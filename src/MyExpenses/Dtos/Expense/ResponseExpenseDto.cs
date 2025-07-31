namespace MyExpenses.Dtos.Expense
{
    public record ResponseExpenseDto(
        Guid Id,
        decimal Value,
        DateOnly Date,
        string Category);
    
}
