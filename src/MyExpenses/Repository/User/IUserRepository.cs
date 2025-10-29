using MyExpenses.Data.UnitOfWork;
using MyExpenses.Models;

namespace MyExpenses.Repository.User;

public interface IUserRepository
{
    IUnitOfWork UnitOfWork { get; }
    void SignUpUser(UserModel user);
    Task<UserModel> FindUserByGuid(Guid userId);
    Task<UserModel> FindUserByEmail(string email);
    Task<UserModel> FindUserByCpf(string cpf);
    Task<UserModel> UpdateUser(UserModel user);
    Task DeleteUser(UserModel user);
}