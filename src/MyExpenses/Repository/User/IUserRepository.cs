using MyExpenses.Models;

namespace MyExpenses.Repository.User;

public interface IUserRepository
{
    Task SignUpUser(UserModel user);
}