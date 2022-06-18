using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DBContext.Context;
using Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Services.Configurations;

namespace Services.Implementations;

public class JwtService: IJwtService
{
    private readonly string _secret;
    private readonly int _expDateInMinutes;
    private readonly TravelDbContext _context;
    
    public JwtService(IOptions<JwtConfiguration> options, TravelDbContext context)
    {
        _context = context;
        _secret = options.Value.Secret;
        _expDateInMinutes = options.Value.ExpirationInMinutes;
    }
    public async Task<(string, string)> GenerateSecurityTokenAsync(string id)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(_expDateInMinutes),
            Audience = "localhost",
            Issuer = "localhost",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
      
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var refreshToken = new RefreshToken()
        {
            JwtId = token.Id,
            UserId = id,
            CreationDate = DateTime.Now,
            ExpireDate = DateTime.UtcNow.AddMonths(6)
        };
        refreshToken.JwtId = token.Id;
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
        return (tokenHandler.WriteToken(token), refreshToken.Token);
    }
}