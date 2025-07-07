using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyExpenses.Models;

namespace MyExpenses.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryModel>
    {
        public void Configure(EntityTypeBuilder<CategoryModel> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(p => p.UserId);
                
        }
    }
}
