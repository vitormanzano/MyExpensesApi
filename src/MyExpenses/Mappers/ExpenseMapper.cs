using MyExpenses.Dtos.Expense;
using MyExpenses.Models;

namespace MyExpenses.Mappers
{
    public static class ExpenseMapper
    {
        public static ResponseExpenseDto MapExpenseToResponseExpenseDto(this ExpenseModel expense)
        {
            var expenseResponse = new ResponseExpenseDto(
                expense.Id,
                expense.Value,
                expense.Description,
                expense.Date,
                expense.Category.Id);

            return expenseResponse;
        }
    }
}
