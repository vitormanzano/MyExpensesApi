using MyExpenses.Dtos.User;
using MyExpenses.Exceptions.User;
using MyExpenses.Jwt;
using MyExpenses.Mappers;
using MyExpenses.Models;
using MyExpenses.Repository.User;
using MyExpenses.Services.Exceptions;
using MyExpenses.Results;
using MyExpenses.Errors.Users;

namespace MyExpenses.Services.User;

public class UserService(IUserRepository userRepository, TokenProvider tokenProvider) : IUserService
{
    public async Task<Result> SignUp(SignUpUserDto signUpUserDto)
    {
        var userWithEmail = await userRepository.FindUserByEmail(signUpUserDto.Email);

        if (userWithEmail is not null)
            return UsersErrors.EmailAlreadyExists;

        var userWithCpf = await userRepository.FindUserByCpf(signUpUserDto.Cpf);

        if (userWithCpf is not null)
            return UsersErrors.CpfAlreadyExists;

        var user = new UserModel(
            signUpUserDto.Cpf,
            signUpUserDto.Email,
            signUpUserDto.Password);

        await userRepository.SignUpUser(user);
        var inserted = await userRepository.UnitOfWork.CommitAsync();

        if (!inserted) 
            return UsersErrors.SignUpFailed;
        return Result.Ok;
    }

    public async Task<Result<string>> Login(LoginUserDto loginUserDto)
    {
        var user = await userRepository.FindUserByEmail(loginUserDto.Email);
        if (user is null) return UsersErrors.NotFound;

        var passwordMatches = user.Password.Verify(loginUserDto.Password);

        if (!passwordMatches)
            return UsersErrors.CredentialsWrong;

        return tokenProvider.Create(user);
    }

    public async Task<Result<ResponseUserDto>> FindUserByEmail(string email)
    {
        var user = await userRepository.FindUserByEmail(email);
        if (user is null) return UsersErrors.NotFound;

        return user.MapUserToResponseUserDto();
    }

    public async Task<Result<ResponseUserDto>> FindUserByCpf(string cpf)
    {
        var user = await userRepository.FindUserByCpf(cpf);
        if (user is null) return UsersErrors.NotFound;

        return user.MapUserToResponseUserDto();
    }

    public async Task<Result<UpdateUserDto>> UpdateUserByGuid(UpdateUserDto updateUserDto, Guid userId)
    {
        var user = await userRepository.FindUserByGuid(userId);
        if (user is null) return UsersErrors.NotFound;

        user.SetEmail(updateUserDto.Email);
        user.SetPassword(updateUserDto.Password);

        await userRepository.UpdateUser(user);
        
        var updated = await userRepository.UnitOfWork.CommitAsync();
        if (!updated)
            return UsersErrors.UpdateFailed;
        
        return updateUserDto;
    }

    public async Task<Result> DeleteUser(string password, Guid userId)
    {
        var user = await userRepository.FindUserByGuid(userId); 
        if (user is null) return UsersErrors.NotFound;

        var passwordMatches = user.Password.Verify(password);

        if (!passwordMatches)
            return UsersErrors.CredentialsWrong; 
        
        await userRepository.DeleteUser(user);
        
        var deleted = await userRepository.UnitOfWork.CommitAsync();
        if (!deleted)
            return UsersErrors.DeleteFailed;

        return Result.Ok;
    }
}
