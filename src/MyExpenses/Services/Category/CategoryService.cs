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
        public async Task<Result<ResponseCategoryDto>> Create(CreateCategoryDto createCategoryDto, Guid userId)
        {
            var existingCategory = await categoryRepository.FindByName(createCategoryDto.Name, userId);
            if (existingCategory is not null) return CategoriesErrors.AlreadyExists;
            
            var categoryModel = new CategoryModel(createCategoryDto.Name, userId);
            await categoryRepository.Create(categoryModel);

            var inserted = await categoryRepository.UnitOfWork.CommitAsync();

            if (!inserted) 
                return CategoriesErrors.CreateFailed;

            return categoryModel.MapCategoryToResponseCategoryDto();
        }

        public async Task<Result<List<ResponseCategoryDto>>> FindAllByUser(Guid userId)
        {
            var categories = await categoryRepository.FindAllByUser(userId);
            if (!categories.Any()) return CategoriesErrors.NotFound;

            return categories.Select(x => x.MapCategoryToResponseCategoryDto()).ToList();
        }

        public async Task<Result<PagedResultDto<ResponseCategoryDto>>> FindAllByUserPaginated(Guid userId, int page, int pageSize)
        {
            var (categories, totalCount) = await categoryRepository.FindAllByUserPaginated(userId, page, pageSize);
            
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

        public async Task<Result<ResponseCategoryDto>> FindById(Guid categoryId)
        {
            var category = await categoryRepository.FindById(categoryId); 
            if (category is null) return CategoriesErrors.NotFound;

            return category.MapCategoryToResponseCategoryDto();
        }

        public async Task<Result<ResponseCategoryDto>> FindByName(string name, Guid userId)
        {
            var category = await categoryRepository.FindByName(name, userId);
            if (category is null) return CategoriesErrors.NotFound;

            return category.MapCategoryToResponseCategoryDto();
;
        }

        public async Task<Result<ResponseCategoryDto>> UpdateById(Guid id, string name)
        {
            var category = await categoryRepository.FindById(id);
            if (category is null) return CategoriesErrors.NotFound;

            category.SetName(name);

            categoryRepository.UpdateById(category);
            var updated = await categoryRepository.UnitOfWork.CommitAsync();
            if (!updated)
                return CategoriesErrors.UpdateFailed;

            return category.MapCategoryToResponseCategoryDto();
        }
        
        public async Task<Result> DeleteById(Guid userId, Guid categoryId)
        {
            var category = await categoryRepository.FindById(categoryId);             
            if (category is null) return CategoriesErrors.NotFound;

            var verify = await VerifyIfExistExpensesInCategory(userId, category.Id);
            if (verify.isFailure)
                return verify.Error;

            categoryRepository.Delete(category);
            
            var deleted = await categoryRepository.UnitOfWork.CommitAsync();
            if (!deleted)
                return CategoriesErrors.DeleteFailed;

            return Result.Ok;
        }

        public async Task<Result> DeleteByName(string name, Guid userId)
        {
            var category = await categoryRepository.FindByName(name, userId);            
            if (category is null) return CategoriesErrors.NotFound;

            var verify = await VerifyIfExistExpensesInCategory(userId, category.Id);
            if (verify.isFailure)
                return verify.Error;

            categoryRepository.Delete(category);
            
            var deleted = await categoryRepository.UnitOfWork.CommitAsync();
            if (!deleted)
                return CategoriesErrors.DeleteFailed;

            return Result.Ok;
        }

        private async Task<Result> VerifyIfExistExpensesInCategory(Guid userId, Guid categoryId)
        {
            var expenses =  await expenseRepository.FindByCategory(userId, categoryId);
            
            if (expenses.Any())
                return CategoriesErrors.HasExpense;

            return Result.Ok;
                
        }
    }
}
