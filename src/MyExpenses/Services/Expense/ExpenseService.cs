using MyExpenses.Dtos.Expense;
using MyExpenses.Mappers;
using MyExpenses.Models;
using MyExpenses.Repository.Expense;
using MyExpenses.Services.Exceptions;
using MyExpenses.Errors.Expenses;
using MyExpenses.Results;

namespace MyExpenses.Services.Expense
{
    public class ExpenseService(IExpenseRepository expenseRepository) : IExpenseService
    {
        public async Task<Result<ResponseExpenseDto>> CreateExpense(CreateExpenseDto createExpenseDto, Guid userId)
        {
            var expenseModel = new ExpenseModel(
                createExpenseDto.Value,
                createExpenseDto.Description,
                createExpenseDto.Date,
                userId,
                createExpenseDto.CategoryId);

            var expenseDb = await expenseRepository.CreateExpense(expenseModel);

            var inserted = await expenseRepository.UnitOfWork.CommitAsync();
            if (!inserted)
                return ExpenseErrors.CreateFailed;
            
            return expenseDb.MapExpenseToResponseExpenseDto();
        }

        public async Task<Result<List<ResponseExpenseDto>>> FindAllExpenses(Guid userId)
        {
            var expenses = await expenseRepository.FindAllExpenses(userId);
            return expenses.Select(x => x.MapExpenseToResponseExpenseDto()).ToList();
        }

        public async Task<Result<ResponseExpenseDto>> FindExpenseById(Guid id, Guid userId)
        {
            var expense = await expenseRepository.FindExpenseById(id, userId);
            if (expense is null) return ExpenseErrors.NotFound;
            
            return expense.MapExpenseToResponseExpenseDto();
        }

        public async Task<Result<List<ResponseExpenseDto>>> FindExpensesByValue(Guid userId, decimal value)
        {
            if (value <= 0)
                return ExpenseErrors.ValueLowerThanZero;

            var expenses = await expenseRepository.FindExpensesByValue(userId, value);
            
            if (!expenses.Any())
                return ExpenseErrors.NotFound;
            
            return expenses.Select(x => x.MapExpenseToResponseExpenseDto()).ToList();
        }
        
        public async Task<Result<List<ResponseExpenseDto>>> FindExpensesByCategory(Guid userId, Guid categoryId)
        {
            var expenses = await expenseRepository.FindExpensesByCategory(userId, categoryId);
            
            if (!expenses.Any())
                return ExpenseErrors.NotFound;
            return expenses.Select(x => x.MapExpenseToResponseExpenseDto()).ToList();
        }

        public async Task<Result<List<ResponseExpenseDto>>> FindExpensesByMonth(Guid userId, int month, int year)
        {
            if (month < 1 || month > 12)
                return ExpenseErrors.InvalidMonth;
            
            if (year < 1900 || year > DateTime.Now.Year + 10)
                return ExpenseErrors.InvalidYear;
            
            var startDate = new DateOnly(year, month, 1);
            var endDate = startDate.AddMonths(1);
            
            var expenses = await expenseRepository.FindExpensesByDate(userId, startDate, endDate);
            
            if (!expenses.Any())
                return ExpenseErrors.NotFound;
            
            return expenses.Select(x => x.MapExpenseToResponseExpenseDto()).ToList();
        }

        public async Task<Result<ResponseExpenseDto>> UpdateExpenseById(UpdateExpenseDto updateExpenseDto, Guid userId)
        {
            var expenseExist = await expenseRepository.FindExpenseById(updateExpenseDto.ExpenseId, userId);
            
            if (expenseExist is null) 
                return ExpenseErrors.NotFound;
            
            expenseExist.SetValue(updateExpenseDto.Value);
            expenseExist.SetDate(updateExpenseDto.Date);
            expenseExist.SetCategoryId(updateExpenseDto.CategoryId);
            
            var updatedExpense = await expenseRepository.UpdateExpense(expenseExist);
            
            var updated = await expenseRepository.UnitOfWork.CommitAsync();
            if (!updated)
                return ExpenseErrors.UpdateFailed;
            
            return updatedExpense.MapExpenseToResponseExpenseDto();
        }

        public async Task<Result> DeleteExpense(Guid expenseId, Guid userId)
        {
            var expense = await expenseRepository.FindExpenseById(expenseId, userId); 
            if (expense is null) return ExpenseErrors.DeleteFailed;

            await expenseRepository.DeleteExpense(expense);
            
            var deleted = await expenseRepository.UnitOfWork.CommitAsync();
            if (!deleted)
                return ExpenseErrors.DeleteFailed;

            return Result.Ok;
        }
    }
}
