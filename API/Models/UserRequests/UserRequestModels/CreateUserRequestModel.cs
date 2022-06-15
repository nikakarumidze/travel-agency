namespace API.Models.UserRequests.UserRequestModels;

public class CreateUserRequestModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Firstname  { get; set; }
    public string Lastname { get; set; }
    public byte[]? Image { get; set; }
}