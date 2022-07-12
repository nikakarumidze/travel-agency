using Domain.POCOs;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;

namespace Repositories.Implementations;

public class CityRepository : ICityRepository
{
    private readonly IBaseRepository<City> _baseRepository;

    public CityRepository(IBaseRepository<City> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public async Task<City> GetCityByNameAsync(string name)
    {
        return await _baseRepository.TableAsNoTracking
            .SingleOrDefaultAsync(x => x.Name == name);
    }

    public async Task<List<City>> GetAllCitiesAsync()
    {
        return await _baseRepository.GetAllAsync();
    }

    public async Task<City> CreateCityAsync(City city)
    {
        var obj = await _baseRepository.CreateAsync(city);
        return obj;
    }
}