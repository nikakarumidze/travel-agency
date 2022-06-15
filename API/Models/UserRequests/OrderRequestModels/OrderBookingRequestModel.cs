using System.ComponentModel.DataAnnotations;
using API.Infrastructure.CustomDataAnnotations;

namespace API.Models.UserRequests.OrderRequestModels;

public class OrderBookingRequestModel
{
    /// <summary>
    /// Format - YY-MM-DD
    /// </summary>
    [Required]
    [DateValidation("Invalid Date, given value is in the past.")]
    public DateTime From { get; set; }
    /// <summary>
    /// Format - YY-MM-DD
    /// </summary>
    [Required]
    [DateValidation("Invalid Date, given value is in the past.")]
    public DateTime To { get; set; }
    
    [Required]
    [Unlike("HostId")]
    public string GuestId { get; set; }
    [Required]
    [Unlike("GuestId")]
    public string HostId { get; set; }
}