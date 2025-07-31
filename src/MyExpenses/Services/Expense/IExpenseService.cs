using MyExpenses.Dtos.Expense;

namespace MyExpenses.Services.Expense
{
    public interface IExpenseService
    {
        Task<ResponseExpenseDto> CreateExpense(CreateExpenseDto createExpenseDto, Guid UserId);
        Task<List<ResponseExpenseDto>> FindAllExpenses(Guid userId);
        Task<ResponseExpenseDto> FindExpenseById(Guid id);
        Task<List<ResponseExpenseDto>> FindExpensesByValue(Guid userId, decimal value);
        Task<List<ResponseExpenseDto>> FindExpenseByMonth(Guid userId, int month, int year);
        Task<ResponseExpenseDto> UpdateExpenseById(UpdateExpenseDto updateExpenseDto);
    }
}
