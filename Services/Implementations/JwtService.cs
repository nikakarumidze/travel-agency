using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Services.Configurations;

namespace Services.Implementations;

public class JwtService: IJwtService
{
    private readonly string _secret;
    private readonly int _expDateInMinutes;
        
    public JwtService(IOptions<JwtConfiguration> options)
    {
        _secret = options.Value.Secret;
        _expDateInMinutes = options.Value.ExpirationInMinutes;
    }
    public (string,DateTime?) GenerateSecurityToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
            }),
            Expires = DateTime.UtcNow.AddMinutes(_expDateInMinutes),
            Audience = "localhost",
            Issuer = "localhost",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return (tokenHandler.WriteToken(token), tokenDescriptor.Expires);
    }
}