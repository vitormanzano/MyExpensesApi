using MyExpenses.Data.UnitOfWork;
using MyExpenses.Models;

namespace MyExpenses.Repository.Expense
{
    public interface IExpenseRepository
    {
        IUnitOfWork UnitOfWork { get; }
        Task<ExpenseModel> Create(ExpenseModel expense);
        Task<List<ExpenseModel>> FindAll(Guid userid);
        Task<ExpenseModel> FindById(Guid id, Guid userId);
        Task<List<ExpenseModel>> FindByValue(Guid userId, decimal value);
        Task<List<ExpenseModel>> FindByDate(Guid userId, DateOnly startDate, DateOnly endDate);
        Task<List<ExpenseModel>> FindByCategory(Guid userId, Guid categoryId);
        Task<ExpenseModel> Update(ExpenseModel expense);
        Task Delete(ExpenseModel expense);
    }
}
