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
                user.Email.EmailAddress); //Email is a value object in UserModel, so we go in the class Email to get the value

            return userResponse;
        }
    }
}
