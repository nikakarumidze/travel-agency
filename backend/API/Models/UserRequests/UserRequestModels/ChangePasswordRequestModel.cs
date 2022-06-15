namespace API.Models.UserRequests.UserRequestModels;

public class ChangePasswordRequestModel
{
    public string Username { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}