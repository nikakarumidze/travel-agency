using Services.Models;

namespace Services.Abstractions;

public interface IApartmentService
{
    Task<ApartmentServiceModel> GetMyApartmentAsync(string id);
    Task<int> CreateApartmentAsync(ApartmentServiceModel request);
}