using System.Reflection.Metadata;
using MyExpenses.ValueObjects;

namespace MyExpenses.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Cpf { get; set; }
        public EmailVO Email { get; set; }
        public PasswordVO Password { get; set; }
        public List<ExpenseModel> Expenses { get; set; } = new List<ExpenseModel>();
        public List<CategoryModel> Categories { get; set; }
        
        protected UserModel() { }
    }
}
