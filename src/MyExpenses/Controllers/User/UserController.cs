using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyExpenses.Dtos.User;
using MyExpenses.Services.Exceptions;
using MyExpenses.Services.User;

namespace MyExpenses.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpUserDto user)
        {
            try
            {
                await userService.SignUp(user);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("FindByEmail/{email}")]
        public async Task<IActionResult> FindUserByEmail(string email)
        {
            try
            {
                var user = await userService.FindUserByEmail(email);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    NotFoundException => NotFound(ex.Message),
                    UnauthorizedAccessException => Unauthorized(ex.Message),
                    _ => BadRequest(ex.Message),
                };
            }
        }

        [Authorize]
        [HttpGet("FindByCpf/{cpf}")]
        public async Task<IActionResult> FindUserByCpf(string cpf)
        {
            try
            {
                var user = await userService.FindUserByCpf(cpf);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    NotFoundException => NotFound(ex.Message),
                    UnauthorizedAccessException => Unauthorized(ex.Message),
                    _ => BadRequest(ex.Message),
                };
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                var token = await userService.Login(loginUserDto);
                return Ok("Token: " + token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("Update/{cpf}")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto updateUserDto, string cpf)
        {
            try
            {
                var user = await userService.UpdateUserByCpf(updateUserDto, cpf);
                return Ok(user);
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
        [HttpDelete("Delete/{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            try
            {
                await userService.DeleteUserByEmail(email);
                return Ok("User deleted!");
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    NotFoundException => NotFound(ex.Message),
                    UnauthorizedAccessException => Unauthorized(ex.Message),
                    _ => BadRequest(ex.Message),
                };
            }
        }
    }
}
