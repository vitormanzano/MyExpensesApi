namespace MyExpenses.Dtos.Expense
{
    public record ResponseExpenseDto(
        decimal Value,
        DateOnly Date,
        Guid UserId,
        string Category);
    
}
