using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

namespace MyExpenses.ValueObjects
{
    public class PasswordVo
    {
        public string PasswordValue { get; private set; }

        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;

        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

        protected PasswordVo() { }

        public PasswordVo(string passwordValue)
        {
            SetPassword(passwordValue);
        }

        public void SetPassword(string passwordValue)
        {
            ValidatePassword(passwordValue);
            PasswordValue = EncryptPassword(passwordValue);
        }

        private void ValidatePassword(string password)
        {
            switch (password.Length)
            {
                case < 4:
                    throw new ArgumentException("Password must be at least 4 characters");

                case > 255:
                    throw new ArgumentException("Password cannot exceed 255 characters");
            }
        }

        private string EncryptPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
        }

        public bool Verify(string password, string encryptedPassword)
        {
            string[] parts = encryptedPassword.Split('-');
            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
    }
}
