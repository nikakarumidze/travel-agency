using Services.Models;
using Services.Models.ServiceModels;

namespace Services.Abstractions;

public interface ICityService
{
    Task<CityServiceModel> GetCityByNameAsync(string city);
    Task<List<CityServiceModel>> GetAllCitiesAsync();
    Task<int> CreateCityAsync(CityServiceModel city);
}