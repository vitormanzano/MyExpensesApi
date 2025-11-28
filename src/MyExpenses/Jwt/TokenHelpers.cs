using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MyExpenses.Jwt;

public static class TokenHelpers
{
    public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
    {
        var tokenKey = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]);

        return new TokenValidationParameters()
        {
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidateAudience = true,
            ValidAudience = configuration["Jwt:Audience"],
            ValidateIssuer = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(tokenKey)
        }; // Verify If token is valid
    }
}