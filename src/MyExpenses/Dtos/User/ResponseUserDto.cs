namespace MyExpenses.Dtos.User
{
    public class ResponseUserDto
    {
        public Guid Id { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ResponseUserDto(Guid id, string cpf, string email, string password) 
        { 
            Id = id;
            Cpf = cpf;
            Email = email;
            Password = password;
        }
    }
}
