using Microsoft.EntityFrameworkCore;
using MyExpenses.Data;
using MyExpenses.Data.UnitOfWork;
using MyExpenses.Models;

namespace MyExpenses.Repository.User;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task SignUpUser(UserModel user)
    {
        await context.Users.AddAsync(user);
    }

    public async Task<UserModel> FindUserByGuid(Guid userId)
    {
        var user = await context.Users.FindAsync(userId);
        return user;
    }

    public async Task<UserModel> FindUserByEmail(string email)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email.EmailAddress.Equals(email));
        return user;
    }

    public async Task<UserModel> FindUserByCpf(string cpf)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Cpf.Equals(cpf));
        return user;
    }

    public async Task<UserModel> UpdateUser(UserModel user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();

        return user;
    }

    public async Task DeleteUser(UserModel user)
    {
        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }
}