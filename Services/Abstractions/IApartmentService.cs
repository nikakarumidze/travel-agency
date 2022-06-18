using Domain;
using Services.Models;
using Services.Models.ServiceModels;

namespace Services.Abstractions;

public interface IApartmentService
{
    Task<List<ApartmentServiceModel>> GetAllAsync(PaginationFilter pagination);
    Task<List<ApartmentServiceModel>> GetAllWithBusyDatesAsync(PaginationFilter pagination);
    Task<ApartmentServiceModel> GetWithBusyDatesAsync(int apartmentId);

    Task<List<ApartmentServiceModel>> GetAllByCityAsync(string city, PaginationFilter pagination);
    Task<List<ApartmentServiceModel>> GetAllByAddressAsync(string address);
    Task<ApartmentServiceModel> GetMineAsync();
    Task<int> CreateMineAsync(ApartmentServiceModel request);
    Task UpdateMineAsync(ApartmentServiceModel request);
    Task DeleteMineAsync();

    Task<List<ApartmentServiceModel>> Search(ApartmentSearchServiceModel search, PaginationFilter pagination);
}