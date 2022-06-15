namespace Services.Abstractions;

public interface  IJwtService
{
    (string, DateTime?) GenerateSecurityToken(string username);
}