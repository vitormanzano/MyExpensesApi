using MyExpenses.Models;

namespace MyExpensesTest;

public class UserModelTests
{
    [Fact]
    public void Create_AnUser()
    {
        var user = new UserModel("12345678901", "johndoe@gmail.com", "123456");
        Assert.NotNull(user);
    }

    [Fact]
    public void Should_ThrowAnException_WhenTryToCreateAUser_CpfIsVoidOrEmpty()
    {
        const string invalidCpf = "";

        var ex = Assert.Throws<ArgumentException>(() =>
            new UserModel(invalidCpf, "johndoe@gmail.com", "123456"));
        
        Assert.Equal("Cpf cannot be void", ex.Message);
    }
    
    [Fact]
    public void Should_ThrowAnException_WhenTryToCreateAUser_CpfLengthIsNot11()
    {
        const string invalidCpf = "1234567890";

        var ex = Assert.Throws<ArgumentException>(() =>
            new UserModel(invalidCpf, "johndoe@gmail.com", "123456"));
        
        Assert.Equal("Cpf must have 11 characters", ex.Message);
    }
}