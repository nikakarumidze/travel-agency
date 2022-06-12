using Domain.POCOs;

namespace Repositories.Abstractions;

public interface ICityRepository
{
    Task<City> GetCityByNameAsync(string city);
    Task<List<City>> GetAllCitiesAsync();
    Task<int> CreateCityAsync(City city);
}