using MyExpenses.Dtos.Expense;
using MyExpenses.Mappers;
using MyExpenses.Models;
using MyExpenses.Repository.Expense;

namespace MyExpenses.Services.Expense
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<ResponseExpenseDto> CreateExpense(CreateExpenseDto createExpenseDto, Guid UserId)
        {
            var expenseModel = new ExpenseModel(
                createExpenseDto.Value,
                createExpenseDto.Date,
                UserId,
                createExpenseDto.CategoryId);

            var expenseDb = await _expenseRepository.CreateExpense(expenseModel);

            var expenseResponse = expenseDb.MapExpenseToResponseExpenseDto();

            return expenseResponse;
        }

        public async Task<List<ResponseExpenseDto>> FindAllExpenses(Guid userId)
        {
            var expenses = await _expenseRepository.FindAllExpenses(userId);

            var expensesResponse = expenses.Select(x => x.MapExpenseToResponseExpenseDto()).ToList();

            return expensesResponse;
        }
    }
}
