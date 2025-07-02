using System.Reflection.Metadata;

namespace MyExpenses.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        protected UserModel() { }
    }
}
