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
                .Where(x => x.UserId.Equals(userId))
                .ToListAsync();

            return categories;
        }

        public async Task<CategoryModel> FindCategoryById(Guid categoryId)
        {
            var category = await context.Categories
                .FindAsync(categoryId);

            return category;
        }

        public async Task<CategoryModel> FindCategoryByName(string name)
        {

            var category = await context.Categories
                .FirstOrDefaultAsync(x => x.Name.Equals(name));
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
