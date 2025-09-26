using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Service.Catalogue.Api.Services;
public class JwtTokenService
{
    private const string SecretKey = "3PEEo1LXa1fm6SjNq7LejZwMng2O3k5J"; // must match Program.cs
    private readonly byte[] _key = Encoding.UTF8.GetBytes(SecretKey);

    public string GenerateToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("role", "admin")
            }),
            Expires = DateTime.UtcNow.AddHours(1), // valid for 1 hour
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
