namespace API.Models.UserRequests.UserRequestModels;

public class RefreshTokenRequest
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}