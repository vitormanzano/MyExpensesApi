using MyExpenses.Dtos.User;
using MyExpenses.Results;

namespace MyExpenses.Services.User;

public interface IUserService
{
    Task<Result> SignUp(SignUpUserDto signUpUserDto);
    Task<Result<string>> Login(LoginUserDto loginUserDto);
    Task<Result<ResponseUserDto>> FindByEmail(string email);
    Task<Result<ResponseUserDto>> FindByCpf(string cpf);
    Task<Result<UpdateUserDto>> UpdateByGuid(UpdateUserDto updateUserDto, Guid userId);
    Task<Result> Delete(string password, Guid userId);
}
