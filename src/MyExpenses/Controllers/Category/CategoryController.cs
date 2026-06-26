using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyExpenses.Dtos.Category;
using MyExpenses.Dtos.Common;
using MyExpenses.Services.Category;
using MyExpenses.Services.Exceptions;
using MyExpenses.UserContext;
using MyExpenses.Results;

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
            var userId = userContext.UserId;

            var result = await categoryService.CreateCategory(categoryDto, userId);
            return result.Match(_ => Created(), error => error.ToActionResult(this));
        }

        [Authorize]
        [HttpGet("FindAllCategories")]
        public async Task<IActionResult> FindAllCategories()
        {
            var userId = userContext.UserId;

            var result = await categoryService.FindAllCategoriesByUser(userId);
            return result.Match(categories => Ok(categories), error => error.ToActionResult(this));
        }
        
        [Authorize]
        [HttpGet("FindAllCategoriesPaginated")]
        public async Task<IActionResult> FindAllCategories([FromQuery] PaginationDto paginationDto)
        {
            var userId = userContext.UserId;

            var result = await categoryService.FindAllCategoriesByUserPaginated(userId, paginationDto.Page, paginationDto.PageSize);
            return result.Match(categories => Ok(categories), error => error.ToActionResult(this));
        }
        
        [Authorize]
        [HttpGet("FindCategoryById/{categoryId}")]
        public async Task<IActionResult> FindCastegoryById(Guid categoryId)
        {
            var userId = userContext.UserId;

            var result = await categoryService.FindCategoryById(categoryId);
            return result.Match(category => Ok(category), error => error.ToActionResult(this));
        }
        
        [Authorize]
        [HttpGet("FindCategoryByName/{name}")]
        public async Task<IActionResult> FindCastegoryByName(string name)
        {
            var userId = userContext.UserId;

            var result = await categoryService.FindCategoryByName(name, userId);
            return result.Match(category => Ok(category), error => error.ToActionResult(this));
        }
        
        [Authorize]
        [HttpPatch("UpdateCategoryById/{categoryId}")]
        public async Task<IActionResult> UpdateCategoryById(Guid categoryId, [FromBody] string nameToUpdate)
        {
            var userId = userContext.UserId;

            var result = await categoryService.UpdateCategoryById(categoryId, nameToUpdate);
            return result.Match(category => Ok(category), error => error.ToActionResult(this));
        }
        
        [Authorize]
        [HttpDelete("DeleteCategoryById/{categoryId}")]
        public async Task<IActionResult> DeleteCategoryById(Guid categoryId)
        {
            var userId = userContext.UserId;

            var result = await categoryService.DeleteCategoryById(userId, categoryId);
            return result.Match(() => Ok(), error => error.ToActionResult(this));
        }

        [Authorize]
        [HttpDelete("DeleteCategoryByName/{name}")]
        public async Task<IActionResult> DeleteCategoryByName(string name)
        {
            var userId = userContext.UserId;

            var result = await categoryService.DeleteCategoryByName(name, userId);
            return result.Match(() => Ok(), error => error.ToActionResult(this));
        }
    }
}
