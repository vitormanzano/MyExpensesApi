using System.Reflection.Metadata;
using MyExpenses.ValueObjects;

namespace MyExpenses.Models
{
    public class UserModel
    {
        public Guid Id { get; private set; }
        public string Cpf { get; private set; } = string.Empty;
        public EmailVO Email { get; private set; } = null!;
        public PasswordVO Password { get; private set; } = null!;
        public List<CategoryModel> Categories = [];
        public List<ExpenseModel> Expenses = [];
        
        protected UserModel() { }

        public UserModel(string cpf, string email, string password)
        {
            Id = Guid.NewGuid();
            SetCpf(cpf);
            SetEmail(email);
            SetPassword(password);
        }

        public void SetCpf(string cpf)
        {
            ValidateCpf(cpf);
            Cpf = cpf;
        }

        private static void ValidateCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                throw new ArgumentException("Cpf cannot be void");

            if (cpf.Length != 11)
                throw new ArgumentException("Cpf must have 11 characters");
        }

        public void SetEmail(string email)
        {
            Email = new EmailVO(email);
        }

        public void SetPassword(string password)
        {
            Password = new PasswordVO(password);
        }
    }
}
