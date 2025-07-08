using Microsoft.AspNetCore.Http.HttpResults;
using MyExpenses.Dtos.User;
using MyExpenses.Mappers;
using MyExpenses.Models;
using MyExpenses.Repository.User;
using MyExpenses.Services.Exceptions;

namespace MyExpenses.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task SignUp(SignUpUserDto signUpUserDto)
    {
        var user = new UserModel(
            signUpUserDto.Cpf,
            signUpUserDto.Email,
            signUpUserDto.Password);

        await _userRepository.SignUpUser(user);
    }

    public async Task<ResponseUserDto> FindUserByEmail(string email)
    {
        var user = await _userRepository.FindUserByEmail(email) ?? throw new NotFoundException("User not found!");
        var userFormatted = user.MapUserToResponseUserDto();

        return userFormatted;
    }
}