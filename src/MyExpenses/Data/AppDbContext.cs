using Microsoft.EntityFrameworkCore;
using MyExpenses.Data.Configurations;
using MyExpenses.Data.UnitOfWork;
using MyExpenses.Models;

namespace MyExpenses.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IUnitOfWork
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ExpenseModel> Expenses { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }

        public async Task<bool> CommitAsync()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.State.GetType().GetProperty("CreatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync() > 0;
        }
    }
}
