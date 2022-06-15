using Services.Models.DTOs;

namespace Services.Models.ServiceModels;

public class OrderServiceModel
{
    public int Id { get; set; }
    public DateTime IssuedAt { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public bool? Approved { get; set; }
    
    public AppUserSimple Guest { get; set; }
    public AppUserSimple Host { get; set; }
    
    public string GuestId { get; set; }
    public string HostId { get; set; }

    public bool isDeleted { get; set; } = false;
}