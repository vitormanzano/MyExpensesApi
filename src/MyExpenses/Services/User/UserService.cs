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
        var userWithEmail = await userRepository.FindByEmail(signUpUserDto.Email);

        if (userWithEmail is not null)
            return UsersErrors.EmailAlreadyExists;

        var userWithCpf = await userRepository.FindByCpf(signUpUserDto.Cpf);

        if (userWithCpf is not null)
            return UsersErrors.CpfAlreadyExists;

        var user = new UserModel(
            signUpUserDto.Cpf,
            signUpUserDto.Email,
            signUpUserDto.Password);

        await userRepository.SignUp(user);
        var inserted = await userRepository.UnitOfWork.CommitAsync();

        if (!inserted) 
            return UsersErrors.SignUpFailed;
        return Result.Ok;
    }

    public async Task<Result<string>> Login(LoginUserDto loginUserDto)
    {
        var user = await userRepository.FindByEmail(loginUserDto.Email);
        if (user is null) return UsersErrors.NotFound;

        var passwordMatches = user.Password.Verify(loginUserDto.Password);

        if (!passwordMatches)
            return UsersErrors.CredentialsWrong;

        return tokenProvider.Create(user);
    }

    public async Task<Result<ResponseUserDto>> FindByEmail(string email)
    {
        var user = await userRepository.FindByEmail(email);
        if (user is null) return UsersErrors.NotFound;

        return user.MapUserToResponseUserDto();
    }

    public async Task<Result<ResponseUserDto>> FindByCpf(string cpf)
    {
        var user = await userRepository.FindByCpf(cpf);
        if (user is null) return UsersErrors.NotFound;

        return user.MapUserToResponseUserDto();
    }

    public async Task<Result<UpdateUserDto>> UpdateByGuid(UpdateUserDto updateUserDto, Guid userId)
    {
        var user = await userRepository.FindByGuid(userId);
        if (user is null) return UsersErrors.NotFound;

        user.SetEmail(updateUserDto.Email);
        user.SetPassword(updateUserDto.Password);

        await userRepository.Update(user);
        
        var updated = await userRepository.UnitOfWork.CommitAsync();
        if (!updated)
            return UsersErrors.UpdateFailed;
        
        return updateUserDto;
    }

    public async Task<Result> Delete(string password, Guid userId)
    {
        var user = await userRepository.FindByGuid(userId); 
        if (user is null) return UsersErrors.NotFound;

        var passwordMatches = user.Password.Verify(password);

        if (!passwordMatches)
            return UsersErrors.CredentialsWrong; 
        
        await userRepository.Delete(user);
        
        var deleted = await userRepository.UnitOfWork.CommitAsync();
        if (!deleted)
            return UsersErrors.DeleteFailed;

        return Result.Ok;
    }
}
