using System.ComponentModel.DataAnnotations;

namespace API.Models.UserRequests.UserRequestModels;

public class CreateUserRequestModel
{
    [Required]
    public string UserName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required(AllowEmptyStrings=false)] 
    public string Password { get; set; }
    [Required]
    public string Firstname  { get; set; }
    [Required]
    public string Lastname { get; set; }
    /// <summary>
    /// send as base64  . . .
    /// </summary>
    public string? Image { get; set; }
}