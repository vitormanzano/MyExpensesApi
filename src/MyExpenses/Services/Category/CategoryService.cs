using MyExpenses.Dtos.Category;
using MyExpenses.Dtos.Common;
using MyExpenses.Mappers;
using MyExpenses.Models;
using MyExpenses.Repository.Category;
using MyExpenses.Repository.Expense;
using MyExpenses.Services.Exceptions;
using MyExpenses.Results;
using MyExpenses.Errors.Categories;

namespace MyExpenses.Services.Category
{
    public class CategoryService(ICategoryRepository categoryRepository,
                                 IExpenseRepository expenseRepository) : ICategoryService
    {
        public async Task<Result<ResponseCategoryDto>> CreateCategory(CreateCategoryDto createCategoryDto, Guid userId)
        {
            var existingCategory = await categoryRepository.FindCategoryByName(createCategoryDto.Name, userId);
            if (existingCategory is not null) return CategoriesErrors.AlreadyExists;
            
            var categoryModel = new CategoryModel(createCategoryDto.Name, userId);
            await categoryRepository.CreateCategory(categoryModel);

            var inserted = await categoryRepository.UnitOfWork.CommitAsync();

            if (!inserted) 
                return CategoriesErrors.CreateFailed;

            return categoryModel.MapCategoryToResponseCategoryDto();
        }

        public async Task<Result<List<ResponseCategoryDto>>> FindAllCategoriesByUser(Guid userId)
        {
            var categories = await categoryRepository.FindAllCategoriesByUser(userId);
            if (!categories.Any()) return CategoriesErrors.NotFound;

            return categories.Select(x => x.MapCategoryToResponseCategoryDto()).ToList();
        }

        public async Task<Result<PagedResultDto<ResponseCategoryDto>>> FindAllCategoriesByUserPaginated(Guid userId, int page, int pageSize)
        {
            var (categories, totalCount) = await categoryRepository.FindAllCategoriesByUserPaginated(userId, page, pageSize);
            
            var categoriesResponse = categories.Select(x => x.MapCategoryToResponseCategoryDto()).ToList();
            
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);             
            
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

        public async Task<Result<ResponseCategoryDto>> FindCategoryById(Guid categoryId)
        {
            var category = await categoryRepository.FindCategoryById(categoryId); 
            if (category is null) return CategoriesErrors.NotFound;

            return category.MapCategoryToResponseCategoryDto();
        }

        public async Task<Result<ResponseCategoryDto>> FindCategoryByName(string name, Guid userId)
        {
            var category = await categoryRepository.FindCategoryByName(name, userId);
            if (category is null) return CategoriesErrors.NotFound;

            return category.MapCategoryToResponseCategoryDto();
;
        }

        public async Task<Result<ResponseCategoryDto>> UpdateCategoryById(Guid id, string name)
        {
            var category = await categoryRepository.FindCategoryById(id);
            if (category is null) return CategoriesErrors.NotFound;

            category.SetName(name);

            categoryRepository.UpdateCategoryById(category);
            var updated = await categoryRepository.UnitOfWork.CommitAsync();
            if (!updated)
                return CategoriesErrors.UpdateFailed;

            return category.MapCategoryToResponseCategoryDto();
        }
        
        public async Task<Result> DeleteCategoryById(Guid userId, Guid categoryId)
        {
            var category = await categoryRepository.FindCategoryById(categoryId);             
            if (category is null) return CategoriesErrors.NotFound;

            var verify = await VerifyIfExistExpensesInCategory(userId, category.Id);
            if (verify.isFailure)
                return verify.Error;

            categoryRepository.DeleteCategory(category);
            
            var deleted = await categoryRepository.UnitOfWork.CommitAsync();
            if (!deleted)
                return CategoriesErrors.DeleteFailed;

            return Result.Ok;
        }

        public async Task<Result> DeleteCategoryByName(string name, Guid userId)
        {
            var category = await categoryRepository.FindCategoryByName(name, userId);            
            if (category is null) return CategoriesErrors.NotFound;

            var verify = await VerifyIfExistExpensesInCategory(userId, category.Id);
            if (verify.isFailure)
                return verify.Error;

            categoryRepository.DeleteCategory(category);
            
            var deleted = await categoryRepository.UnitOfWork.CommitAsync();
            if (!deleted)
                return CategoriesErrors.DeleteFailed;

            return Result.Ok;
        }

        private async Task<Result> VerifyIfExistExpensesInCategory(Guid userId, Guid categoryId)
        {
            var expenses =  await expenseRepository.FindExpensesByCategory(userId, categoryId);
            
            if (expenses.Any())
                return CategoriesErrors.HasExpense;

            return Result.Ok;
                
        }
    }
}
