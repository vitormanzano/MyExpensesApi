using Microsoft.EntityFrameworkCore;
using MyExpenses.Data;
using MyExpenses.Data.UnitOfWork;
using MyExpenses.Models;

namespace MyExpenses.Repository.User;

public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;

    public IUnitOfWork UnitOfWork => _context;

    public void SignUpUser(UserModel user)
    {
        _context.Users.Add(user);
    }

    public async Task<UserModel> FindUserByGuid(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        return user;
    }

    public async Task<UserModel> FindUserByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.EmailAddress.Equals(email));
        return user;
    }

    public async Task<UserModel> FindUserByCpf(string cpf)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Cpf.Equals(cpf));
        return user;
    }

    public async Task<UserModel> UpdateUser(UserModel user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task DeleteUser(UserModel user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}