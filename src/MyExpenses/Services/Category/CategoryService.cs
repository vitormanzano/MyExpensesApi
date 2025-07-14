using MyExpenses.Dtos.Category;
using MyExpenses.Mappers;
using MyExpenses.Models;
using MyExpenses.Repository.Category;
using MyExpenses.Services.Exceptions;

namespace MyExpenses.Services.Category
{
    public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {
        public async Task<ResponseCategoryDto> CreateCategory(CreateCategoryDto createCategoryDto, Guid userId)
        {
            var categoryModel = new CategoryModel(createCategoryDto.Name, userId);
            await categoryRepository.CreateCategory(categoryModel);

            var categoryFormatted = categoryModel.MapCategoryToResponseCategoryDto();
            return categoryFormatted;
        }

        public async Task<List<ResponseCategoryDto>> FindAllCategoriesByUser(Guid userId)
        {
            var categories = await categoryRepository.FindAllCategoriesByUser(userId);
            var categoriesResponse = categories.Select(x => x.MapCategoryToResponseCategoryDto()).ToList();

            return categoriesResponse;
        }

        public async Task<ResponseCategoryDto> FindCategoryById(Guid id)
        {
            var category = await categoryRepository.FindCategoryById(id) ?? throw new NotFoundException("Category not found!");

            var categoryResponse = category.MapCategoryToResponseCategoryDto();
            return categoryResponse;
        }

        public async Task<ResponseCategoryDto> FindCategoryByName(string name)
        {
            var category = await categoryRepository.FindCategoryByName(name) ?? throw new NotFoundException("Category not found!");

            var categoryResponse = category.MapCategoryToResponseCategoryDto();
            return categoryResponse;
        }

        public async Task<ResponseCategoryDto> UpdateCategoryById(Guid id, string name)
        {
            var category = await categoryRepository.FindCategoryById(id) ?? throw new NotFoundException("Category not found!");
            category.SetName(name);

            await categoryRepository.UpdateCategoryById(category);

            var categoryResponse = category.MapCategoryToResponseCategoryDto();
            return categoryResponse;
        }


        public async Task DeleteCategoryById(Guid id)
        {
            var category = await categoryRepository.FindCategoryById(id) ?? throw new NotFoundException("Category not found!");
            await categoryRepository.DeleteCategory(category);
        }

        public async Task DeleteCategoryByName(string name)
        {
            var category = await categoryRepository.FindCategoryByName(name) ?? throw new NotFoundException("Category not found!");
            await categoryRepository.DeleteCategory(category);
        }
    }
}
