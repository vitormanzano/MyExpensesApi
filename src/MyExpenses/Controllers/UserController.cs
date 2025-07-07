using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyExpenses.Dtos.User;
using MyExpenses.Services.User;

namespace MyExpenses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpUserDto user)
        {
            try
            {
                await _userService.SignUp(user);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
