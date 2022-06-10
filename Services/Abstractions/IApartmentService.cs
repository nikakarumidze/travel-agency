using Services.Models;

namespace Services.Abstractions;

public interface IApartmentService
{
    Task<List<ApartmentServiceModel>> GetAllAsync();
    Task<List<ApartmentServiceModel>> GetAllByCityAsync(string city);
    Task<List<ApartmentServiceModel>> GetAllByAddressAsync(string address);
    Task<ApartmentServiceModel> GetMyApartmentAsync(string id);
    Task<int> CreateApartmentAsync(ApartmentServiceModel request);
    Task UpdateApartmentAsync(ApartmentServiceModel request);
}