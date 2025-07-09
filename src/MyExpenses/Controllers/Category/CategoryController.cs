using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyExpenses.Dtos.Category;
using MyExpenses.Services.Category;

namespace MyExpenses.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto categoryDto)
        {
            try
            {
                var category = await _categoryService.CreateCategory(categoryDto);
                return Created("", categoryDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
