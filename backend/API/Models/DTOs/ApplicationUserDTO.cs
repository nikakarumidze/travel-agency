namespace API.Models.DTOs;

public class ApplicationUserDTO
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public byte[] Image { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Bio { get; set; }
    
    public ApartmentWONavProperties Apartment { get; set; }
    public List<OrderDTO> MyHosts { get; set; }
    public List<OrderDTO> MyTravels { get; set; }
}