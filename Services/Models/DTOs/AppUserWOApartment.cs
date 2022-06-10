namespace Services.Models.DTOs;

public class AppUserWOApartment
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public byte[] Image { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PhoneNumber { get; set; }
}