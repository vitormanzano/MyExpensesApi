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

        public async Task<ResponseExpenseDto> CreateExpense(CreateExpenseDto createExpenseDto)
        {
            var expenseModel = new ExpenseModel(
                createExpenseDto.Value,
                createExpenseDto.Date,
                createExpenseDto.UserId,
                createExpenseDto.CategoryId);

            var expenseDb = await _expenseRepository.CreateExpense(expenseModel);

            var expenseResponse = expenseDb.MapExpenseToResponseExpenseDto();

            return expenseResponse;
        }
    }
}
