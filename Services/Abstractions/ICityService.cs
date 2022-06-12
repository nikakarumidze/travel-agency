using Services.Models;

namespace Services.Abstractions;

public interface ICityService
{
    Task<CityServiceModel> GetCityByNameAsync(string city);
    Task<List<CityServiceModel>> GetAllCitiesAsync();
    Task<int> CreateCityAsync(CityServiceModel city);
}