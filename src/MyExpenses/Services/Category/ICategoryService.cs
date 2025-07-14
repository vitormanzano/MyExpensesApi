using MyExpenses.Dtos.Category;

namespace MyExpenses.Services.Category
{
    public interface ICategoryService
    {
        Task<ResponseCategoryDto> CreateCategory(CreateCategoryDto createCategoryDto, Guid userId);
        Task<List<ResponseCategoryDto>> FindAllCategoriesByUser(Guid userId);
        Task<ResponseCategoryDto> FindCategoryById(Guid id);
        Task<ResponseCategoryDto> FindCategoryByName(string name);
        Task<ResponseCategoryDto> UpdateCategoryById(Guid id, string name);
        Task DeleteCategoryById(Guid id);
    }
}
