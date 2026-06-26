using MyExpenses.Dtos.User;
using MyExpenses.Results;

namespace MyExpenses.Services.User;

public interface IUserService
{
    Task<Result> SignUp(SignUpUserDto signUpUserDto);
    Task<Result<string>> Login(LoginUserDto loginUserDto);
    Task<Result<ResponseUserDto>> FindUserByEmail(string email);
    Task<Result<ResponseUserDto>> FindUserByCpf(string cpf);
    Task<Result<UpdateUserDto>> UpdateUserByGuid(UpdateUserDto updateUserDto, Guid userId);
    Task<Result> DeleteUser(string password, Guid userId);
}
