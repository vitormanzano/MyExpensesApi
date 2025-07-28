using MyExpenses.Dtos.Expense;
using MyExpenses.Mappers;
using MyExpenses.Models;
using MyExpenses.Repository.Expense;
using MyExpenses.Services.Exceptions;

namespace MyExpenses.Services.Expense
{
    public class ExpenseService(IExpenseRepository expenseRepository) : IExpenseService
    {
        public async Task<ResponseExpenseDto> CreateExpense(CreateExpenseDto createExpenseDto, Guid userId)
        {
            var expenseModel = new ExpenseModel(
                createExpenseDto.Value,
                createExpenseDto.Date,
                userId,
                createExpenseDto.CategoryId);

            var expenseDb = await expenseRepository.CreateExpense(expenseModel);

            var expenseResponse = expenseDb.MapExpenseToResponseExpenseDto();

            return expenseResponse;
        }

        public async Task<List<ResponseExpenseDto>> FindAllExpenses(Guid userId)
        {
            var expenses = await expenseRepository.FindAllExpenses(userId);

            var expensesResponse = expenses.Select(x => x.MapExpenseToResponseExpenseDto()).ToList();
            return expensesResponse;
        }

        public async Task<ResponseExpenseDto> FindExpenseById(Guid id)
        {
            var expense = await expenseRepository.FindExpenseById(id) ?? throw new NotFoundException("Expense not found!");
            
            var expenseResponse = expense.MapExpenseToResponseExpenseDto();
            return expenseResponse;
        }

        public async Task<List<ResponseExpenseDto>> FindExpensesByValue(Guid userId, decimal value)
        {
            if (value <= 0)
                throw new ArgumentException("Expense value must be greater than zero!");

            var expenses = await expenseRepository.FindExpensesByValue(userId, value);
            
            if (expenses.Count == 0)
                throw new NotFoundException("None expense was found!");
            
            var expensesResponse = expenses.Select(x => x.MapExpenseToResponseExpenseDto()).ToList();
            return expensesResponse;
        }

        public async Task<List<ResponseExpenseDto>> FindExpenseByMonth(Guid userId, int month, int year)
        {
            if (month < 1 || month > 12)
                throw new ArgumentException("Month must be between 1 and 12!");
            
            if (year < 1900 || year > DateTime.Now.Year + 10)
                throw new ArgumentException("Invalid year!");
            
            var startDate = new DateOnly(year, month, 1);
            var endDate = startDate.AddMonths(1);
            
            var expenses = await expenseRepository.FindExpensesByDate(userId, startDate, endDate);
            
            if (expenses.Count == 0)
                throw new NotFoundException("None expense was found!");
            
            var expensesResponse = expenses.Select(x => x.MapExpenseToResponseExpenseDto()).ToList();
            return expensesResponse;
        }
    }
}
