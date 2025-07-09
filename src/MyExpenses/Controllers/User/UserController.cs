using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyExpenses.Dtos.User;
using MyExpenses.Services.Exceptions;
using MyExpenses.Services.User;

namespace MyExpenses.Controllers.User
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

        [HttpGet("FindByEmail/{email}")]
        public async Task<IActionResult> FindUserByEmail(string email)
        {
            try
            {
                var user = await _userService.FindUserByEmail(email);
                return Ok(user);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case NotFoundException:
                        return NotFound(ex.Message);
                    default:
                        return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("FindByCpf/{cpf}")]
        public async Task<IActionResult> FindUserByCpf(string cpf)
        {
            try
            {
                var user = await _userService.FindUserByCpf(cpf);
                return Ok(user);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case NotFoundException:
                        return NotFound(ex.Message);
                    default:
                        return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                var token = await _userService.Login(loginUserDto);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("Update/{cpf}")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto updateUserDto, string cpf)
        {
            try
            {
                var user = await _userService.UpdateUserByCpf(updateUserDto, cpf);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            try
            {
                await _userService.DeleteUserByEmail(email);
                return Ok("User deleted!");
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case NotFoundException:
                        return NotFound(ex.Message);
                    default:
                        return BadRequest(ex.Message);
                }
            }
        }
    }
}
