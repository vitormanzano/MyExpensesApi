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

        public async Task<ResponseCategoryDto> CreateCategory(CreateCategoryDto createCategoryDto, Guid userId)
        {
            var categoryModel = new CategoryModel(createCategoryDto.Name, userId);

            await _categoryRepository.CreateCategory(categoryModel);

            var categoryFormatted = categoryModel.MapCategoryToResponseCategoryDto();

            return categoryFormatted;
        }

        public async Task<List<ResponseCategoryDto>> FindAllCategoriesByUser(Guid userId)
        {
            var categories = await _categoryRepository.FindAllCategoriesByUser(userId);
            var categoriesResponse = categories.Select(x => x.MapCategoryToResponseCategoryDto()).ToList();

            return categoriesResponse;
        }
    }
}
