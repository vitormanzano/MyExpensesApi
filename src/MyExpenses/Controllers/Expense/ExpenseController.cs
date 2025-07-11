using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyExpenses.Dtos.Expense;
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
                return BadRequest(ex);
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
                    Exception => Unauthorized(ex.Message),
                    _ => BadRequest(ex.Message)
                };
            }

        }
    }
}
