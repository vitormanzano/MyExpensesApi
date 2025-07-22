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
                return Created("Category created!", category);
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
        public async Task<IActionResult> FindAllCategories()
        {
            try
            {
                var userId = userContext.UserId;

                var categories = await categoryService.FindAllCategoriesByUser(userId);
                return Ok(categories);
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
        [HttpGet("FindCategoryById/{id}")]
        public async Task<IActionResult> FindCastegoryById(Guid categoryId)
        {
            try
            {
                var category = await categoryService.FindCategoryById(categoryId);
                return Ok(category);
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
        [HttpGet("FindCategoryByName/{name}")]
        public async Task<IActionResult> FindCastegoryByName(string name)
        {
            try
            {
                var userId = userContext.UserId;
                
                var category = await categoryService.FindCategoryByName(name, userId);
                return Ok(category);
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
        [HttpPatch("UpdateCategoryById/{id}")]
        public async Task<IActionResult> UpdateCategoryById(Guid categoryId, [FromBody] string nameToUpdate)
        {
            try
            {
                var category = await categoryService.UpdateCategoryById(categoryId, nameToUpdate);
                return Ok(category);
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
        [HttpDelete("DeleteCategoryById/{id}")]
        public async Task<IActionResult> DeleteCategoryById(Guid categoryId)
        {
            try
            {
                await categoryService.DeleteCategoryById(categoryId);
                return Ok();
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
        [HttpDelete("DeleteCategoryByName/{name}")]
        public async Task<IActionResult> DeleteCategoryByName(string name)
        {
            try
            {
                var userId = userContext.UserId;
                
                await categoryService.DeleteCategoryByName(name, userId);
                return Ok();
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
