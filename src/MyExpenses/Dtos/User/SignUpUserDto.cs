namespace MyExpenses.Dtos.User;

public record SignUpUserDto(
    string Cpf, 
    string Email, 
    string Password);
