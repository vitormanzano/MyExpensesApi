using MyExpenses.Dtos.User;
using MyExpenses.Jwt;
using MyExpenses.Mappers;
using MyExpenses.Models;
using MyExpenses.Repository.User;
using MyExpenses.Services.Exceptions;

namespace MyExpenses.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly TokenProvider _tokenProvider;

    public UserService(IUserRepository userRepository, TokenProvider tokenProvider)
    {
        _userRepository = userRepository;
        _tokenProvider = tokenProvider;
    }

    public async Task SignUp(SignUpUserDto signUpUserDto)
    {
        var userWithEmail = await _userRepository.FindUserByEmail(signUpUserDto.Email);

        if (userWithEmail is not null)
            throw new Exception("A user with this email already exists!");

        var userWithCpf = await _userRepository.FindUserByCpf(signUpUserDto.Cpf);

        if (userWithCpf is not null)
            throw new Exception("A user with this cpf already exists!");

        var user = new UserModel(
            signUpUserDto.Cpf,
            signUpUserDto.Email,
            signUpUserDto.Password);

        await _userRepository.SignUpUser(user);
    }

    public async Task<string> Login(LoginUserDto loginUserDto)
    {
        var user = await _userRepository.FindUserByEmail(loginUserDto.Email) ?? throw new NotFoundException("User not found!");

        var passwordMatches = user.Password.Verify(loginUserDto.Password, user.Password.PasswordValue);

        if (!passwordMatches)
            throw new Exception("Wrong Password!");

        var token = _tokenProvider.Create(user);

        return token;
    }

    public async Task<ResponseUserDto> FindUserByEmail(string email)
    {
        var user = await _userRepository.FindUserByEmail(email) ?? throw new NotFoundException("User not found!");
        var userFormatted = user.MapUserToResponseUserDto();

        return userFormatted;
    }

    public async Task<ResponseUserDto> FindUserByCpf(string cpf)
    {
        var user = await _userRepository.FindUserByCpf(cpf) ?? throw new NotFoundException("User not found!");
        var userFormatted = user.MapUserToResponseUserDto();

        return userFormatted;
    }

    public async Task<UpdateUserDto> UpdateUserByCpf(UpdateUserDto updateUserDto, string cpf)
    {
        var user = await _userRepository.FindUserByCpf(cpf);

        if (user is null)
            throw new NotFoundException("User not found!");

        user.SetEmail(updateUserDto.Email);
        user.SetPassword(updateUserDto.Password);

        await _userRepository.UpdateUser(user);

        return updateUserDto;
    }

    public async Task DeleteUserByEmail(string email)
    {
        var user = await _userRepository.FindUserByEmail(email);

        if (user is null)
            throw new NotFoundException("User not found!");

        await _userRepository.DeleteUser(user);
    }
}