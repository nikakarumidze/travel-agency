namespace API.Models.DTOs;

public class ApartmentWONavProperties
{
    public int Id { get; set; }
    public string Address { get; set;}
    public string DistanceToCenter { get; set; }
    public int MaxGuest { get; set; }
    public string Description { get; set; }
    public string ImageBase64 { get; set; }
    public bool? Wifi { get; set; }
    public bool? Pool { get; set; }
    public bool? Gym { get; set; }
    public bool? Conditioner { get; set; }
    public bool? Parking { get; set; }
}