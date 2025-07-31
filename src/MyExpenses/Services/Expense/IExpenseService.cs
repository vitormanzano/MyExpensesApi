using MyExpenses.Dtos.Expense;
using MyExpenses.Models;

namespace MyExpenses.Services.Expense
{
    public interface IExpenseService
    {
        Task<ResponseExpenseDto> CreateExpense(CreateExpenseDto createExpenseDto, Guid UserId);
        Task<List<ResponseExpenseDto>> FindAllExpenses(Guid userId);
        Task<ResponseExpenseDto> FindExpenseById(Guid id, Guid userId);
        Task<List<ResponseExpenseDto>> FindExpensesByValue(Guid userId, decimal value);
        Task<List<ResponseExpenseDto>> FindExpensesByMonth(Guid userId, int month, int year);
        Task<List<ResponseExpenseDto>> FindExpensesByCategory(Guid userId, Guid categoryId);
        Task<ResponseExpenseDto> UpdateExpenseById(UpdateExpenseDto updateExpenseDto, Guid userId);
        Task DeleteExpense(Guid expenseId, Guid userId);
    }
}
