using Microsoft.AspNetCore.Mvc;
using MyExpenses.Dtos.Expense;
using MyExpenses.Services.Expense;

namespace MyExpenses.Controllers.Expense
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateExpenseDto createExpenseDto)
        {
            try
            {
                await _expenseService.CreateExpense(createExpenseDto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
