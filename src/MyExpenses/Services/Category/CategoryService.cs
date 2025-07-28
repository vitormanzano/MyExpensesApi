using MyExpenses.Dtos.Category;
using MyExpenses.Dtos.Common;
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
            var existingCategory = await categoryRepository.FindCategoryByName(createCategoryDto.Name, userId);

            if (existingCategory != null)
            {
                throw new InvalidOperationException("Category with this name already exists!");
            }
            
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

        public async Task<PagedResultDto<ResponseCategoryDto>> FindAllCategoriesByUserPaginated(Guid userId, int page, int pageSize)
        {
            var (categories, totalCount) = await categoryRepository.FindAllCategoriesByUserPaginated(userId, page, pageSize);
            
            var categoriesResponse = categories.Select(x => x.MapCategoryToResponseCategoryDto()).ToList();
            
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // Divide, cast to double the division,Ceiling round up the result and cast to int
            
            return new PagedResultDto<ResponseCategoryDto>
            {
                Data = categoriesResponse,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalCount,
                TotalPages = totalPages,
                HasPreviousPage = page > 1,
                HasNextPage = page < totalPages
            };
        }

        public async Task<ResponseCategoryDto> FindCategoryById(Guid id)
        {
            var category = await categoryRepository.FindCategoryById(id) ?? throw new NotFoundException("Category not found!");

            var categoryResponse = category.MapCategoryToResponseCategoryDto();
            return categoryResponse;
        }

        public async Task<ResponseCategoryDto> FindCategoryByName(string name, Guid userId)
        {
            var category = await categoryRepository.FindCategoryByName(name, userId) ?? throw new NotFoundException("Category not found!");

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

        public async Task DeleteCategoryByName(string name, Guid userId)
        {
            var category = await categoryRepository.FindCategoryByName(name, userId) ?? throw new NotFoundException("Category not found!");
            await categoryRepository.DeleteCategory(category);
        }
    }
}
