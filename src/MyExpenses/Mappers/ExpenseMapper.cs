using MyExpenses.Dtos.Expense;
using MyExpenses.Models;

namespace MyExpenses.Mappers
{
    public static class ExpenseMapper
    {
        public static ResponseExpenseDto MapExpenseToResponseExpenseDto(this ExpenseModel expense)
        {
            var expenseResponse = new ResponseExpenseDto(
                expense.Value,
                expense.Date,
                expense.UserId,
                expense.Category.Name);

            return expenseResponse;
        }
    }
}
