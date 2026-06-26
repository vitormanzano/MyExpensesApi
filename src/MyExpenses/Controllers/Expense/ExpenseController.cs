using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyExpenses.Dtos.Expense;
using MyExpenses.Services.Exceptions;
using MyExpenses.Services.Expense;
using MyExpenses.UserContext;
using MyExpenses.Results;

namespace MyExpenses.Controllers.Expense
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController(IExpenseService expenseService, IUserContext userContext) : ControllerBase
    {
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateExpenseDto createExpenseDto)
        {
            var userId = userContext.UserId;

            var result = await expenseService.CreateExpense(createExpenseDto, userId);
            return result.Match(_ => Created(), error => error.ToActionResult(this));
        }

        [Authorize]
        [HttpGet("FindAllExpensesByUser")]
        public async Task<IActionResult> FindAllExpensesByUser()
        {
            var userId = userContext.UserId;

            var result = await expenseService.FindAllExpenses(userId);
            return result.Match(expenses => Ok(expenses), error => error.ToActionResult(this));
        }

        [Authorize]
        [HttpGet("FindExpenseById/{expenseId}")]
        public async Task<IActionResult> FindExpenseById([FromRoute] Guid expenseId)
        {
            var userId = userContext.UserId;

            var result = await expenseService.FindExpenseById(expenseId, userId);
            return result.Match(expense => Ok(expense), error => error.ToActionResult(this));
        }
        
        [Authorize]
        [HttpGet("FindExpensesByValue/{value}")]
        public async Task<IActionResult> FindExpensesByValue([FromRoute] decimal value)
        {
            var userId = userContext.UserId;

            var result = await expenseService.FindExpensesByValue(userId, value);
            return result.Match(expense => Ok(expense), error => error.ToActionResult(this));
        }
        
        [Authorize]
        [HttpGet("FindExpensesByMonth")]
        public async Task<IActionResult> FindExpensesByMonth([FromQuery] int month, [FromQuery] int year)
        {
            var userId = userContext.UserId;

            var result = await expenseService.FindExpensesByMonth(userId, month, year);
            return result.Match(expense => Ok(expense), error => error.ToActionResult(this));
        }

        [Authorize]
        [HttpGet("FindExpenseByCategory/{categoryId}")]
        public async Task<IActionResult> FindExpenseByCategory([FromRoute] Guid categoryId)
        {
            var userId = userContext.UserId;

            var result = await expenseService.FindExpensesByCategory(userId, categoryId);
            return result.Match(expenses => Ok(expenses), error => error.ToActionResult(this));
        }
        
        [Authorize]
        [HttpPut("UpdateExpenseById")]
        public async Task<IActionResult> UpdateExpenseById([FromBody] UpdateExpenseDto updateExpenseDto)
        {
            var userId = userContext.UserId;

            var result = await expenseService.UpdateExpenseById(updateExpenseDto, userId);
            return result.Match(expense => Ok(expense), error => error.ToActionResult(this));
        }

        [Authorize]
        [HttpDelete("DeleteExpenseById/{id}")]
        public async Task<IActionResult> DeleteExpenseById([FromRoute] Guid id)
        {
            var userId = userContext.UserId;

            var result = await expenseService.DeleteExpense(id, userId);
            return result.Match(() => Ok(), error => error.ToActionResult(this));
        }
    }
}
