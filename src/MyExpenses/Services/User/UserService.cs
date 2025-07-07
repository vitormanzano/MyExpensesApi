using MyExpenses.Dtos.User;
using MyExpenses.Models;
using MyExpenses.Repository.User;

namespace MyExpenses.Services.User;

public class UserService  : IUserService
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
}