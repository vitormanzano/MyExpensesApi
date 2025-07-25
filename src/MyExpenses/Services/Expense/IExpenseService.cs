﻿using MyExpenses.Dtos.Expense;

namespace MyExpenses.Services.Expense
{
    public interface IExpenseService
    {
        Task<ResponseExpenseDto> CreateExpense(CreateExpenseDto createExpenseDto, Guid UserId);
        Task<List<ResponseExpenseDto>> FindAllExpenses(Guid userId);
    }
}
