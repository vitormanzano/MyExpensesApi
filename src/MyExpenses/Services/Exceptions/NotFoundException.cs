namespace MyExpenses.Services.Exceptions
{
    public class NotFoundException(string message) : Exception(message)
    {
    }
}
