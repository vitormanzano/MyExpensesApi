using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyExpenses.Models;

namespace MyExpenses.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Cpf)
                .HasMaxLength(11)
                .IsRequired();

            builder.OwnsOne(p => p.Email)
                .Property(p => p.EmailAddress)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired();


            builder.OwnsOne(p => p.Password)
                .Property(p => p.PasswordValue)
                .HasColumnName("password")
                .HasMaxLength(255)
                .IsRequired();

            builder
                .HasMany(p => p.Expenses)
                .WithOne(e => e.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(p => p.Categories)
                .WithOne(c => c.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
