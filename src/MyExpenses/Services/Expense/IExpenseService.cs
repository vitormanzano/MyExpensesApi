using MyExpenses.Dtos.Expense;
using MyExpenses.Models;
using MyExpenses.Results;

namespace MyExpenses.Services.Expense
{
    public interface IExpenseService
    {
        Task<Result<ResponseExpenseDto>> Create(CreateExpenseDto createExpenseDto, Guid UserId);
        Task<Result<List<ResponseExpenseDto>>> FindAll(Guid userId);
        Task<Result<ResponseExpenseDto>> FindById(Guid id, Guid userId);
        Task<Result<List<ResponseExpenseDto>>> FindByValue(Guid userId, decimal value);
        Task<Result<List<ResponseExpenseDto>>> FindByMonth(Guid userId, int month, int year);
        Task<Result<List<ResponseExpenseDto>>> FindByCategory(Guid userId, Guid categoryId);
        Task<Result<ResponseExpenseDto>> UpdateById(UpdateExpenseDto updateExpenseDto, Guid userId);
        Task<Result> Delete(Guid expenseId, Guid userId);
    }
}
