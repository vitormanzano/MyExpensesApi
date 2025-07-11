using MyExpenses.Dtos.User;

namespace MyExpenses.Services.User;

public interface IUserService
{
    Task SignUp(SignUpUserDto signUpUserDto);
    Task<string> Login(LoginUserDto loginUserDto);
    Task<ResponseUserDto> FindUserByEmail(string email);
    Task<ResponseUserDto> FindUserByCpf(string cpf);
    Task<UpdateUserDto> UpdateUserByGuid(UpdateUserDto updateUserDto, Guid userId);
    Task DeleteUser(string password, Guid userId);
}