using Microsoft.EntityFrameworkCore;
using MyExpenses.Data;
using MyExpenses.Models;

namespace MyExpenses.Repository.Category
{
    public class CategoryRepository(AppDbContext context) : ICategoryRepository
    {
        public async Task CreateCategory(CategoryModel category)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();
        }

        public async Task<List<CategoryModel>> FindAllCategoriesByUser(Guid userId)
        {
            var categories = await context.Categories
                .AsNoTracking()
                .Where(x => x.UserId == userId) 
                .OrderBy(category => category.Name)
                .ToListAsync();

            return categories;
        }

        public async Task<(List<CategoryModel> Categories, int TotalCount)> FindAllCategoriesByUserPaginated(Guid userId, int page, int pageSize)
        {
            var query = context.Categories //Prepare the query to use
                .AsNoTracking()
                .Where(x => x.UserId == userId);

            // Count how many categories return
            var totalCount = await query.CountAsync();

            // Paginate categories
            var categories = await query
                .OrderBy(category => category.Name)
                .Skip((page - 1) * pageSize) // How much I want to skip | page = 1 | (1 - 1) * 20 = 0
                .Take(pageSize)
                .ToListAsync();

            return (categories, totalCount);
        }

        public async Task<CategoryModel> FindCategoryById(Guid categoryId)
        {
            var category = await context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == categoryId);
            return category;
        }
        
        public async Task<CategoryModel> FindCategoryByName(string name, Guid userId)
        {
            var category = await context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Name.Equals(name) && c.UserId == userId);

            return category;
        }


        public async Task<CategoryModel> UpdateCategoryById(CategoryModel category)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return category;
        }

        public async Task DeleteCategory(CategoryModel category)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }
}
