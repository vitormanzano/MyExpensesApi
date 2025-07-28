namespace MyExpenses.Dtos.Expense
{
    public record ResponseExpenseDto(
        decimal Value,
        DateOnly Date,
        string Category);
    
}
