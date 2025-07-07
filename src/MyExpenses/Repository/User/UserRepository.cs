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
}