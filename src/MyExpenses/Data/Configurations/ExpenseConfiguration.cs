using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyExpenses.Models;

namespace MyExpenses.Data.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<ExpenseModel>
    {
        public void Configure(EntityTypeBuilder<ExpenseModel> builder)
        {
            builder.ToTable("Expenses");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Value)
                .HasColumnType("DECIMAL(18,2)")
                .IsRequired();

            builder.Property(p => p.Date)
                .IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(u => u.Expenses)
                .HasPrincipalKey(u => u.Id);

            builder.HasOne(p => p.Category);
        }
    }
}
