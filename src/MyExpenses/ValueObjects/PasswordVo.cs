using System.Security.Cryptography;

namespace MyExpenses.ValueObjects;

public sealed class PasswordVo
{
    public string PasswordValue { get; } = null!;

    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100_000;
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

    private PasswordVo() { }

    public PasswordVo(string passwordValue)
    {
        ValidatePassword(passwordValue);
        PasswordValue = HashPassword(passwordValue);
    }

    private static void ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty!", nameof(password));

        if (password.Length < 4)
            throw new ArgumentException("Password must be at least 4 characters");

        if (password.Length > 255)
            throw new ArgumentException("Password cannot exceed 255 characters");
    }

    private static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

    public bool Verify(string password)
    {
        string[] parts = PasswordValue.Split('-');

        if (parts.Length != 2)
            throw new Exception("Password strange format.");

        byte[] hash = Convert.FromHexString(parts[0]);
        byte[] salt = Convert.FromHexString(parts[1]);
        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

        return CryptographicOperations.FixedTimeEquals(hash, inputHash);
    }
}
