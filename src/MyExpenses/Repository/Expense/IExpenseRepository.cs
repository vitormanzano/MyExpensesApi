using MyExpenses.Models;

namespace MyExpenses.Repository.Expense
{
    public interface IExpenseRepository
    {
        Task<ExpenseModel> CreateExpense(ExpenseModel expense);
        Task<List<ExpenseModel>> FindAllExpenses(Guid userid);
        Task<ExpenseModel> FindExpenseById(Guid id, Guid userId);
        Task<List<ExpenseModel>> FindExpensesByValue(Guid userId, decimal value);
        Task<List<ExpenseModel>> FindExpensesByDate(Guid userId, DateOnly startDate, DateOnly endDate);
        Task<ExpenseModel> UpdateExpense(ExpenseModel expense);
        Task DeleteExpense(ExpenseModel expense);
    }
}
