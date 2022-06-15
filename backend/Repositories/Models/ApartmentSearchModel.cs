namespace Repositories.Models;

public class ApartmentSearchModel
{
    public DateTime? AvailableFrom { get; set;}
    public DateTime? AvailableTo { get; set; }
    public string? City { get; set; }
    public int? BedsNumber { get; set; }
    public bool? Wifi { get; set; }
    public bool? Pool { get; set; }
    public bool? Gym { get; set; }
    public bool? Conditioner { get; set; }
    public bool? Parking { get; set; }
}