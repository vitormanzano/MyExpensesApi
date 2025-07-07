using Microsoft.EntityFrameworkCore;
using MyExpenses.Data.Configurations;
using MyExpenses.Models;

namespace MyExpenses.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ExpenseModel> Expenses { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
