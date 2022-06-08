using Microsoft.AspNetCore.Identity;

namespace Domain.POCOs;

public class ApplicationUser : IdentityUser
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public byte[]? Image { get; set; }
}