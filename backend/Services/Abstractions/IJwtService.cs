namespace Services.Abstractions;

public interface  IJwtService
{
    Task<(string, string)> GenerateSecurityTokenAsync(string username, string username1);
}