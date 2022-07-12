using Services.Models.DTOs;

namespace Services.Models.ServiceModels;

public class ApartmentServiceModel
{
    public int Id { get; set; }
    public string Address { get; set;}
    public string DistanceToCenter { get; set; }
    public int MaxGuest { get; set; }
    public string Description { get; set; }
    public byte[]? Image { get; set; }
    public bool? Wifi { get; set; }
    public bool? Pool { get; set; }
    public bool? Gym { get; set; }
    public bool? Conditioner { get; set; }
    public bool? Parking { get; set; }
    
    public AppUserSimple Owner { get; set; }
    public string OwnerId { get; set; }

    public string CityName { get; set; }

    public CityServiceModel City { get; set; }
    public int CityId { get; set; }

    public List<DateTime> BusyDates { get; set; }
}