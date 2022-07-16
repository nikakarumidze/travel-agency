using Domain.POCOs;

namespace Repositories.Abstractions;

public interface ICityRepository
{
    Task<City> GetCityByNameAsync(string city);
    Task<List<City>> GetAllCitiesAsync();
    Task<City> CreateCityAsync(City city);
}