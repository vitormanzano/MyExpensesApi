using MyExpenses.Dtos.Category;
using MyExpenses.Models;

namespace MyExpenses.Repository.Category
{
    public interface ICategoryRepository
    {
        Task CreateCategory(CategoryModel category);
    }
}
