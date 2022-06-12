using Domain.POCOs;
using Services.Models.DTOs;

namespace Services.Models;

public class ApartmentServiceModel
{
    public int Id { get; set; }
    public string Address { get; set;}
    public string DistanceToCenter { get; set; }
    public int BedsNumber { get; set; }
    public byte[]? Image { get; set; }
    public bool? Wifi { get; set; }
    public bool? Pool { get; set; }
    public bool? Gym { get; set; }
    public bool? Conditioner { get; set; }
    public bool? Parking { get; set; }
    
    public AppUserWOApartment Owner { get; set; }
    public string OwnerId { get; set; }

    public string CityName { get; set; }

    public CityServiceModel City { get; set; }
    public int CityId { get; set; }
}