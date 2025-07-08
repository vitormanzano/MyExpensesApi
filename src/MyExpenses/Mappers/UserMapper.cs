using MyExpenses.Dtos.User;
using MyExpenses.Models;

namespace MyExpenses.Mappers
{
    public static class UserMapper
    {
        public static ResponseUserDto MapUserToResponseUserDto(this UserModel user)
        {
            var userResponse = new ResponseUserDto(
                user.Id,
                user.Cpf,
                user.Email.EmailAddress, //Email and password are value objects in UserModel, so we go in the class Email to get the value
                user.Password.PasswordValue);

            return userResponse;
        }
    }
}
