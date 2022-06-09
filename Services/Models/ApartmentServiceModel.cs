using Domain.POCOs;

namespace Services.Models;

public class ApartmentServiceModel
{
    public int Id { get; set; }
    public string City { get; set; }
    public string Address { get; set;}
    public string DistanceToCenter { get; set; }
    public int BedsNumber { get; set; }
    public byte[]? Image { get; set; }
    public bool? Wifi { get; set; }
    public bool? Pool { get; set; }
    public bool? Gym { get; set; }
    public bool? Conditioner { get; set; }
    public bool? Parking { get; set; }
    
    public ApplicationUser Owner { get; set; }
    public string OwnerId { get; set; }
}