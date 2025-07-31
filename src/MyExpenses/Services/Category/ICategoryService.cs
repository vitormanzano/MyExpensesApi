using MyExpenses.Dtos.Category;
using MyExpenses.Dtos.Common;

namespace MyExpenses.Services.Category
{
    public interface ICategoryService
    {
        Task<ResponseCategoryDto> CreateCategory(CreateCategoryDto createCategoryDto, Guid userId);
        Task<List<ResponseCategoryDto>> FindAllCategoriesByUser(Guid userId);
        Task<PagedResultDto<ResponseCategoryDto>> FindAllCategoriesByUserPaginated(Guid userId, int page, int pageSize);
        Task<ResponseCategoryDto> FindCategoryById(Guid categoryId);
        Task<ResponseCategoryDto> FindCategoryByName(string name, Guid userId);
        Task<ResponseCategoryDto> UpdateCategoryById(Guid id, string name);
        Task DeleteCategoryById(Guid userId, Guid categoryId);
        Task DeleteCategoryByName(string name, Guid userId);
    }
}
