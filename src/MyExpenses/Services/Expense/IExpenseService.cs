using MyExpenses.Dtos.Expense;
using MyExpenses.Models;
using MyExpenses.Results;

namespace MyExpenses.Services.Expense
{
    public interface IExpenseService
    {
        Task<Result<ResponseExpenseDto>> CreateExpense(CreateExpenseDto createExpenseDto, Guid UserId);
        Task<Result<List<ResponseExpenseDto>>> FindAllExpenses(Guid userId);
        Task<Result<ResponseExpenseDto>> FindExpenseById(Guid id, Guid userId);
        Task<Result<List<ResponseExpenseDto>>> FindExpensesByValue(Guid userId, decimal value);
        Task<Result<List<ResponseExpenseDto>>> FindExpensesByMonth(Guid userId, int month, int year);
        Task<Result<List<ResponseExpenseDto>>> FindExpensesByCategory(Guid userId, Guid categoryId);
        Task<Result<ResponseExpenseDto>> UpdateExpenseById(UpdateExpenseDto updateExpenseDto, Guid userId);
        Task<Result> DeleteExpense(Guid expenseId, Guid userId);
    }
}
