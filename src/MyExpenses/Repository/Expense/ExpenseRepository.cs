using MyExpenses.Data;
using MyExpenses.Models;

namespace MyExpenses.Repository.Expense
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _context;
        
        public ExpenseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ExpenseModel> CreateExpense(ExpenseModel expense)
        {
            var expenseDb = _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return expenseDb.Entity;
        }
    }
}
