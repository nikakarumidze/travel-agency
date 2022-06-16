using System.ComponentModel.DataAnnotations;

namespace API.Models.UserRequests.UserRequestModels;

public class ChangePasswordRequestModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string OldPassword { get; set; }
    [Required]
    public string NewPassword { get; set; }
}