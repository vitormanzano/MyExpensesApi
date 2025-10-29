using MyExpenses.Dtos.User;
using MyExpenses.Jwt;
using MyExpenses.Mappers;
using MyExpenses.Models;
using MyExpenses.Repository.User;
using MyExpenses.Services.Exceptions;

namespace MyExpenses.Services.User;

public class UserService(IUserRepository userRepository, TokenProvider tokenProvider) : IUserService
{
    public async Task SignUp(SignUpUserDto signUpUserDto)
    {
        var userWithEmail = await userRepository.FindUserByEmail(signUpUserDto.Email);

        if (userWithEmail is not null)
            throw new ArgumentException("A user with this email already exists!");

        var userWithCpf = await userRepository.FindUserByCpf(signUpUserDto.Cpf);

        if (userWithCpf is not null)
            throw new ArgumentException("A user with this cpf already exists!");

        var user = new UserModel(
            signUpUserDto.Cpf,
            signUpUserDto.Email,
            signUpUserDto.Password);

        userRepository.SignUpUser(user);
        bool inserted = await userRepository.UnitOfWork.CommitAsync();

        if (!inserted) 
            throw new Exception("Could not sign up user!");

        return;
    }

    public async Task<string> Login(LoginUserDto loginUserDto)
    {
        var user = await userRepository.FindUserByEmail(loginUserDto.Email) ?? throw new NotFoundException("User not found!");

        var passwordMatches = user.Password.Verify(loginUserDto.Password, user.Password.PasswordValue);

        if (!passwordMatches)
            throw new ArgumentException("Wrong Password!");

        var token = tokenProvider.Create(user);

        return token;
    }

    public async Task<ResponseUserDto> FindUserByEmail(string email)
    {
        var user = await userRepository.FindUserByEmail(email) ?? throw new NotFoundException("User not found!");
        var userFormatted = user.MapUserToResponseUserDto();

        return userFormatted;
    }

    public async Task<ResponseUserDto> FindUserByCpf(string cpf)
    {
        var user = await userRepository.FindUserByCpf(cpf) ?? throw new NotFoundException("User not found!");
        var userFormatted = user.MapUserToResponseUserDto();

        return userFormatted;
    }

    public async Task<UpdateUserDto> UpdateUserByGuid(UpdateUserDto updateUserDto, Guid userId)
    {
        var user = await userRepository.FindUserByGuid(userId);

        if (user is null)
            throw new NotFoundException("User not found!");

        user.SetEmail(updateUserDto.Email);
        user.SetPassword(updateUserDto.Password);

        await userRepository.UpdateUser(user);

        return updateUserDto;
    }

    public async Task DeleteUser(string password, Guid userId)
    {
        var user = await userRepository.FindUserByGuid(userId) ?? throw new NotFoundException("User not found!");

        var passwordMatches = user.Password.Verify(password, user.Password.PasswordValue);

        if (!passwordMatches)
            throw new Exception("Wrong Password!");

        await userRepository.DeleteUser(user);
    }
}