using MyExpenses.Dtos.Category;
using MyExpenses.Dtos.Common;

namespace MyExpenses.Services.Category
{
    public interface ICategoryService
    {
        Task<ResponseCategoryDto> CreateCategory(CreateCategoryDto createCategoryDto, Guid userId);
        Task<List<ResponseCategoryDto>> FindAllCategoriesByUser(Guid userId);
        Task<PagedResultDto<ResponseCategoryDto>> FindAllCategoriesByUserPaginated(Guid userId, int page, int pageSize);
        Task<ResponseCategoryDto> FindCategoryById(Guid id);
        Task<ResponseCategoryDto> FindCategoryByName(string name, Guid userId);
        Task<ResponseCategoryDto> UpdateCategoryById(Guid id, string name);
        Task DeleteCategoryById(Guid id);
        Task DeleteCategoryByName(string name, Guid userId);
    }
}
