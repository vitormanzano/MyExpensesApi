﻿using MyExpenses.Models;

namespace MyExpenses.Repository.Expense
{
    public interface IExpenseRepository
    {
        Task<ExpenseModel> CreateExpense(ExpenseModel expense);
        Task<List<ExpenseModel>> FindAllExpenses(Guid userid);
    }
}
