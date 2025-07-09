using MyExpenses.Dtos.Category;

namespace MyExpenses.Services.Category
{
    public interface ICategoryService
    {
        Task<ResponseCategoryDto> CreateCategory(CreateCategoryDto createCategoryDto);
    }
}
