using MyExpenses.Data;
using MyExpenses.Dtos.Category;
using MyExpenses.Models;

namespace MyExpenses.Repository.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateCategory(CategoryModel category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }
    }
}
