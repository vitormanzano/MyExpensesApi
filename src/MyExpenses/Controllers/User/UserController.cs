using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyExpenses.Dtos.User;
using MyExpenses.Services.User;
using MyExpenses.UserContext;
using MyExpenses.Results;

namespace MyExpenses.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService, IUserContext userContext) : ControllerBase
    {
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpUserDto user)
        {
            var result = await userService.SignUp(user);
            return result.Match(() => StatusCode(201), error => error.ToActionResult(this));
        }

        [Authorize]
        [HttpGet("FindByEmail/{email}")]
        public async Task<IActionResult> FindUserByEmail(string email)
        {
            var result = await userService.FindByEmail(email);
            return result.Match(value => Ok(value), error => error.ToActionResult(this));
        }

        [Authorize]
        [HttpGet("FindByCpf/{cpf}")]
        public async Task<IActionResult> FindUserByCpf(string cpf)
        {
            var result = await userService.FindByCpf(cpf);
            return result.Match(value => Ok(value), error => error.ToActionResult(this));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var result = await userService.Login(loginUserDto);
            return result.Match(token => Ok(new { token } ), error => error.ToActionResult(this));
        }

        [Authorize]
        [HttpPatch("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto updateUserDto)
        {
            var userId = userContext.UserId;

            var result = await userService.UpdateByGuid(updateUserDto, userId);
            return result.Match(value => Ok(value), error => error.ToActionResult(this));
        }

        [Authorize]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] string password)
        {
            var userId = userContext.UserId;

            var result = await userService.Delete(password, userId);
            return result.Match(() => Ok(), error => error.ToActionResult(this));
        }
    }
}
