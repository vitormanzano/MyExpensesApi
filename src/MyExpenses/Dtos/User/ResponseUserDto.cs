namespace MyExpenses.Dtos.User
{
    public record ResponseUserDto(
        Guid Id, 
        string Cpf, 
        string Email);
}
