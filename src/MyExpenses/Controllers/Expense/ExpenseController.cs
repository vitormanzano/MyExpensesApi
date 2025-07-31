using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyExpenses.Dtos.Expense;
using MyExpenses.Services.Exceptions;
using MyExpenses.Services.Expense;
using MyExpenses.UserContext;

namespace MyExpenses.Controllers.Expense
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController(IExpenseService expenseService, IUserContext userContext) : ControllerBase
    {
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateExpenseDto createExpenseDto)
        {
            try
            {
                var userId = userContext.UserId;

                await expenseService.CreateExpense(createExpenseDto, userId);
                return Created();
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    UnauthorizedAccessException => Unauthorized(ex.Message),
                    _ => BadRequest(ex.Message)
                };
            }
        }

        [Authorize]
        [HttpGet("FindAllExpensesByUser")]
        public async Task<IActionResult> FindAllExpensesByUser()
        {
            try
            {
                var userId = userContext.UserId;

                var expenses = await expenseService.FindAllExpenses(userId);

                return Ok(expenses);

            }
            catch (Exception ex)
            {
                return ex switch
                {
                    UnauthorizedAccessException => Unauthorized(ex.Message),
                    _ => BadRequest(ex.Message)
                };
            }
        }

        [Authorize]
        [HttpGet("FindExpenseById/{id}")]
        public async Task<IActionResult> FindExpenseById([FromRoute] Guid expenseId)
        {
            try
            {
                var userId = userContext.UserId;
                
                var expenses = await expenseService.FindExpenseById(expenseId, userId);
                return Ok(expenses);

            }
            catch (Exception ex)
            {
                return ex switch
                {
                    UnauthorizedAccessException => Unauthorized(ex.Message),
                    NotFoundException => NotFound(ex.Message),
                    _ => BadRequest(ex.Message)
                };
            }
        }
        
        [Authorize]
        [HttpGet("FindExpensesByValue/{value}")]
        public async Task<IActionResult> FindExpensesByValue([FromRoute] decimal value)
        {
            try
            {
                var userId = userContext.UserId;

                var expenses = await expenseService.FindExpensesByValue(userId, value);
                return Ok(expenses);

            }
            catch (Exception ex)
            {
                return ex switch
                {
                    UnauthorizedAccessException => Unauthorized(ex.Message),
                    NotFoundException => NotFound(ex.Message),
                    _ => BadRequest(ex.Message)
                };
            }
        }
        
        [Authorize]
        [HttpGet("FindExpensesByMonth")]
        public async Task<IActionResult> FindExpensesByMonth([FromQuery] int month, [FromQuery] int year)
        {
            try
            {
                var userId = userContext.UserId;
                var expenses = await expenseService.FindExpenseByMonth(userId, month, year);

                return Ok(expenses);

            }
            catch (Exception ex)
            {
                return ex switch
                {
                    UnauthorizedAccessException => Unauthorized(ex.Message),
                    NotFoundException => NotFound(ex.Message),
                    _ => BadRequest(ex.Message)
                };
            }
        }

        [Authorize]
        [HttpPut("UpdateExpenseById")]
        public async Task<IActionResult> UpdateExpenseById([FromBody] UpdateExpenseDto updateExpenseDto)
        {
            try
            {
                var userId = userContext.UserId;
                
                var updatedExpense = await expenseService.UpdateExpenseById(updateExpenseDto, userId);
                return Ok(updatedExpense);
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    UnauthorizedAccessException => Unauthorized(ex.Message),
                    NotFoundException => NotFound(ex.Message),
                    _ => BadRequest(ex.Message)
                };
            }
        }

        [Authorize]
        [HttpDelete("DeleteExpenseById/{id}")]
        public async Task<IActionResult> DeleteExpenseById([FromRoute] Guid id)
        {
            try
            {
                var userId = userContext.UserId;
                
                await expenseService.DeleteExpense(id, userId);
                return Ok("Expense Deleted");
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    UnauthorizedAccessException => Unauthorized(ex.Message),
                    NotFoundException => NotFound(ex.Message),
                    _ => BadRequest(ex.Message)
                };
            }
        }
    }
}
