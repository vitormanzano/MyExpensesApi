using System.Reflection.Metadata;
using MyExpenses.ValueObjects;

namespace MyExpenses.Models
{
    public class UserModel
    {
        public Guid Id { get; private set; }
        public string Cpf { get; private set; }
        public EmailVO Email { get; private set; }
        public PasswordVO Password { get; private set; }
        public List<ExpenseModel> Expenses { get; set; } = new List<ExpenseModel>();
        public List<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
        
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

        private void ValidateCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                throw new ArgumentException("Cpf cannot be void");

            if (cpf.Length != 11)
                throw new ArgumentException("Cpf must be 11 characters");
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
