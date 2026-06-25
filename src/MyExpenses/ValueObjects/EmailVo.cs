using System.Text.RegularExpressions;

namespace MyExpenses.ValueObjects;

public sealed class EmailVo
{
    public string EmailAddress { get; } = null!;

    private EmailVo() { }

    public EmailVo(string emailAddress )
    {
        ValidateEmail(emailAddress);
        EmailAddress = emailAddress.Trim().ToLowerInvariant(); 
    }

    private static void ValidateEmail(string emailAddress)
    {
        if (emailAddress.Length < 6)
            throw new ArgumentException("Email must be at least 6 characters", nameof(emailAddress));


        if (emailAddress.Length > 255)
            throw new ArgumentException("Email address cannot exceed 255 characters", nameof(emailAddress));

        RegexEmailCheck(emailAddress);
    }

    private static void RegexEmailCheck(string emailAddress)
    {
        var emailIsFormatted = Regex.IsMatch(emailAddress,
                @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

        if (!emailIsFormatted)
            throw new ArgumentException("Email is not valid!", nameof(emailAddress));
    }
}
