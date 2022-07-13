namespace Services.Models.UserRequestServiceModels;

public class CreateUserServiceModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Firstname  { get; set; }
    public string Lastname { get; set; }
    public string Image { get; set; }
}