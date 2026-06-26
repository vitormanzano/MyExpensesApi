using MyExpenses.Dtos.Category;
using MyExpenses.Dtos.Common;
using MyExpenses.Results;

namespace MyExpenses.Services.Category
{
    public interface ICategoryService
    {
        Task<Result<ResponseCategoryDto>> CreateCategory(CreateCategoryDto createCategoryDto, Guid userId);
        Task<Result<List<ResponseCategoryDto>>> FindAllCategoriesByUser(Guid userId);
        Task<Result<PagedResultDto<ResponseCategoryDto>>> FindAllCategoriesByUserPaginated(Guid userId, int page, int pageSize);
        Task<Result<ResponseCategoryDto>> FindCategoryById(Guid categoryId);
        Task<Result<ResponseCategoryDto>> FindCategoryByName(string name, Guid userId);
        Task<Result<ResponseCategoryDto>> UpdateCategoryById(Guid id, string name);
        Task<Result> DeleteCategoryById(Guid userId, Guid categoryId);
        Task<Result> DeleteCategoryByName(string name, Guid userId);
    }
}
