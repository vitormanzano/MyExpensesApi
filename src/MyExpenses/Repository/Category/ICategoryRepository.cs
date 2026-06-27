using MyExpenses.Data.UnitOfWork;
using MyExpenses.Models;

namespace MyExpenses.Repository.Category
{
    public interface ICategoryRepository
    {
        IUnitOfWork UnitOfWork { get; }
        Task Create(CategoryModel category);
        Task<List<CategoryModel>> FindAllByUser(Guid userId);
        Task<CategoryModel> FindById(Guid categoryId);
        Task<CategoryModel> FindByName(string name, Guid userId);
        CategoryModel UpdateById(CategoryModel category);
        void Delete(CategoryModel category);
        Task<(List<CategoryModel> Categories, int TotalCount)> FindAllByUserPaginated(Guid userId, int page, int pageSize);
    }
}
