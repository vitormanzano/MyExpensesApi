using MyExpenses.Dtos.Category;
using MyExpenses.Models;

namespace MyExpenses.Mappers
{
    public static class CategoryMapper
    {
        public static ResponseCategoryDto MapCategoryToResponseCategoryDto(this CategoryModel category)
        {
            var categoryResponse = new ResponseCategoryDto(
                category.Id,
                category.Name);

            return categoryResponse;
        }
    }
}
