using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyExpenses.Dtos.Category;
using MyExpenses.Services.Category;
using MyExpenses.UserContext;

namespace MyExpenses.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService, IUserContext userContext) : ControllerBase
    {
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto categoryDto)
        {
            try
            {
                var userId = userContext.UserId;

                var category = await categoryService.CreateCategory(categoryDto, userId);
                return Created("Category created!", categoryDto);
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    UnauthorizedAccessException => Unauthorized(ex.Message),
                    _ => BadRequest(ex.Message),
                };
            }
        }

        [Authorize]
        [HttpGet("FindAllCategories")]
        public async Task<IActionResult> FindAllCategories([FromBody] CreateCategoryDto categoryDto)
        {
            try
            {
                var userId = userContext.UserId;

                var categories = await categoryService.FindAllCategoriesByUser(userId);
                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    UnauthorizedAccessException => Unauthorized(ex.Message),
                    _ => BadRequest(ex.Message),
                };
            }
        }

    }
}
