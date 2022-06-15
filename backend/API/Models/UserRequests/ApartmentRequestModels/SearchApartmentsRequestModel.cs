using System.ComponentModel.DataAnnotations;
using API.Infrastructure.CustomDataAnnotations;

namespace API.Models.UserRequests.ApartmentRequestModels;

public class SearchApartmentsRequestModel
{
    /// <summary>
    /// Format - YY-MM-DD
    /// </summary>
    [DataType(DataType.Date)]
    [DateValidation("Invalid Date, given value is in the past.")]
    public DateTime? AvailableFrom { get; set;}
    /// <summary>
    /// Format - YY-MM-DD
    /// </summary>
    [DataType(DataType.Date)]
    [DateValidation("Invalid Date, given value is in the past.")]
    public DateTime? AvailableTo { get; set; }
    public string? City { get; set; }
    public int? BedsNumber { get; set; }
    public bool? Wifi { get; set; }
    public bool? Pool { get; set; }
    public bool? Gym { get; set; }
    public bool? Conditioner { get; set; }
    public bool? Parking { get; set; }
}