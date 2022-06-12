using Services.Models;

namespace Services.Abstractions;

public interface IApartmentService
{
    Task<List<ApartmentServiceModel>> GetAllAsync();
    Task<List<ApartmentServiceModel>> GetAllByCityAsync(string city);
    Task<List<ApartmentServiceModel>> GetAllByAddressAsync(string address);
    Task<ApartmentServiceModel> GetMineAsync(string id);
    Task<int> CreateMineAsync(ApartmentServiceModel request);
    Task UpdateMineAsync(ApartmentServiceModel request);
    Task DeleteMineAsync(string userId);
}