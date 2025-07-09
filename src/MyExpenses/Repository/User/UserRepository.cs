using Microsoft.EntityFrameworkCore;
using MyExpenses.Data;
using MyExpenses.Models;

namespace MyExpenses.Repository.User;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public Task SignUpUser(UserModel user)
    {
        _context.Users.Add(user);
        return _context.SaveChangesAsync();
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