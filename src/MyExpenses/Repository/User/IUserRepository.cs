using MyExpenses.Data.UnitOfWork;
using MyExpenses.Models;

namespace MyExpenses.Repository.User;

public interface IUserRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task SignUp(UserModel user);
    Task<UserModel> FindByGuid(Guid userId);
    Task<UserModel> FindByEmail(string email);
    Task<UserModel> FindByCpf(string cpf);
    Task<UserModel> Update(UserModel user);
    Task Delete(UserModel user);
}
