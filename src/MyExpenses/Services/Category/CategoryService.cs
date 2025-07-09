using MyExpenses.Dtos.Category;
using MyExpenses.Mappers;
using MyExpenses.Models;
using MyExpenses.Repository.Category;

namespace MyExpenses.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseCategoryDto> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var categoryModel = new CategoryModel(createCategoryDto.Name, createCategoryDto.UserId);

            await _categoryRepository.CreateCategory(categoryModel);

            var categoryFormatted = categoryModel.MapCategoryToResponseCategoryDto();

            return categoryFormatted;
        }
    }
}
