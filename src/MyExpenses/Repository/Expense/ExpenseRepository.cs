using Microsoft.EntityFrameworkCore;
using MyExpenses.Data;
using MyExpenses.Models;

namespace MyExpenses.Repository.Expense
{
    public class ExpenseRepository(AppDbContext context) : IExpenseRepository
    {
        public async Task<ExpenseModel> CreateExpense(ExpenseModel expense)
        {
            var expenseDb = context.Expenses.Add(expense);
            await context.SaveChangesAsync();

            return expenseDb.Entity;
        }

        public async Task<List<ExpenseModel>> FindAllExpenses(Guid userId)
        {
            var expenses = await context.Expenses
                .AsNoTracking()
                .Include(e => e.Category)
                .Where(e => e.UserId == userId)
                .ToListAsync();

            return expenses;
        }

        public async Task<ExpenseModel> FindExpenseById(Guid id)
        {
            var expense = await context.Expenses
                .AsNoTracking()
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id);
            return expense;
        }

        public async Task<List<ExpenseModel>> FindExpensesByValue(Guid userId, decimal value)
        {
            var expenses = await context.Expenses
                .AsNoTracking()
                .Include(e => e.Category)
                .Where(e => e.UserId == userId &&
                            e.Value == value)
                .ToListAsync();

            return expenses;
        }

        public async Task<List<ExpenseModel>> FindExpensesByDate(Guid userId, DateOnly startDate, DateOnly endDate)
        {
            var expenses = await context.Expenses
                .AsNoTracking()
                .Include(e => e.Category)
                .Where(e => e.UserId == userId && 
                            e.Date >= startDate && // if the date of my expense is higher or equal then my startDate 25 > 20 | it's true, it's between the dates
                            e.Date <= endDate)
                .ToListAsync(); 

            return expenses;
        }
    }
}
