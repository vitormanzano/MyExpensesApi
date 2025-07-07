using MyExpenses.Dtos.User;

namespace MyExpenses.Services.User;

public interface IUserService
{
    Task SignUp(SignUpUserDto signUpUserDto);
}