using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyExpenses.Dtos.Category;
using MyExpenses.Dtos.Common;
using MyExpenses.Services.Category;
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

            var result = await categoryService.Create(categoryDto, userId);
            return result.Match(value => StatusCode(201, value), error => error.ToActionResult(this));
        }

        [Authorize]
        [HttpGet("FindAllCategories")]
        public async Task<IActionResult> FindAllCategories()
        {
            var userId = userContext.UserId;

            var result = await categoryService.FindAllByUser(userId);
            return result.Match(categories => Ok(categories), error => error.ToActionResult(this));
        }
        
        [Authorize]
        [HttpGet("FindAllCategoriesPaginated")]
        public async Task<IActionResult> FindAllCategories([FromQuery] PaginationDto paginationDto)
        {
            var userId = userContext.UserId;

            var result = await categoryService.FindAllByUserPaginated(userId, paginationDto.Page, paginationDto.PageSize);
            return result.Match(categories => Ok(categories), error => error.ToActionResult(this));
        }
        
        [Authorize]
        [HttpGet("FindCategoryById/{categoryId}")]
        public async Task<IActionResult> FindCastegoryById(Guid categoryId)
        {
            var userId = userContext.UserId;

            var result = await categoryService.FindById(categoryId);
            return result.Match(category => Ok(category), error => error.ToActionResult(this));
        }
        
        [Authorize]
        [HttpGet("FindCategoryByName/{name}")]
        public async Task<IActionResult> FindCastegoryByName(string name)
        {
            var userId = userContext.UserId;

            var result = await categoryService.FindByName(name, userId);
            return result.Match(category => Ok(category), error => error.ToActionResult(this));
        }
        
        [Authorize]
        [HttpPatch("UpdateCategoryById/{categoryId}")]
        public async Task<IActionResult> UpdateCategoryById(Guid categoryId, [FromBody] string nameToUpdate)
        {
            var userId = userContext.UserId;

            var result = await categoryService.UpdateById(categoryId, nameToUpdate);
            return result.Match(category => Ok(category), error => error.ToActionResult(this));
        }
        
        [Authorize]
        [HttpDelete("DeleteCategoryById/{categoryId}")]
        public async Task<IActionResult> DeleteCategoryById(Guid categoryId)
        {
            var userId = userContext.UserId;

            var result = await categoryService.DeleteById(userId, categoryId);
            return result.Match(() => Ok(), error => error.ToActionResult(this));
        }

        [Authorize]
        [HttpDelete("DeleteCategoryByName/{name}")]
        public async Task<IActionResult> DeleteCategoryByName(string name)
        {
            var userId = userContext.UserId;

            var result = await categoryService.DeleteByName(name, userId);
            return result.Match(() => Ok(), error => error.ToActionResult(this));
        }
    }
}
