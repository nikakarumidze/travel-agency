using Microsoft.AspNetCore.Identity;

namespace Domain.POCOs;

public class ApplicationUser : IdentityUser
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public byte[]? Image { get; set; }
    public string? Bio { get; set; }

    public Apartment Apartment { get; set; }
    public List<Order> MyTravels { get; set; }
    public List<Order> MyHosts { get; set; }
}