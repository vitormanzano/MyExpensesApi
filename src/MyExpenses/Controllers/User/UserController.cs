using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyExpenses.Dtos.User;
using MyExpenses.Exceptions.User;
using MyExpenses.Services.Exceptions;
using MyExpenses.Services.User;
using MyExpenses.UserContext;

namespace MyExpenses.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService, IUserContext userContext) : ControllerBase
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
                return ex switch
                {
                    UserAlreadyExistsException => Conflict(ex.Message),
                    _ => BadRequest(ex.Message)
                }; 
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
                    UnauthorizedAccessException => Unauthorized(ex.Message),
                    NotFoundException => NotFound(ex.Message),
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
                    UnauthorizedAccessException => Unauthorized(ex.Message),
                    NotFoundException => NotFound(ex.Message),  
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
                return ex switch
                {
                    NotFoundException => NotFound(ex.Message),
                    ArgumentException => Conflict(ex.Message),
                    _ => BadRequest(ex.Message)
                };
            }
        }

        [Authorize]
        [HttpPatch("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto updateUserDto)
        {
            try
            {
                var userId = userContext.UserId;

                var user = await userService.UpdateUserByGuid(updateUserDto, userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    UnauthorizedAccessException => Unauthorized(ex.Message),
                    NotFoundException => NotFound(ex.Message),
                    _ => BadRequest(ex.Message),
                };
            }
        }

        [Authorize]
        [HttpDelete("Delete/{password}")]
        public async Task<IActionResult> Delete(string password)
        {
            try
            {
                var userId = userContext.UserId;

                await userService.DeleteUser(password, userId);
                return Ok("User deleted!");
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    UnauthorizedAccessException => Unauthorized(ex.Message),
                    NotFoundException => NotFound(ex.Message),
                    _ => BadRequest(ex.Message),
                };
            }
        }
    }
}
