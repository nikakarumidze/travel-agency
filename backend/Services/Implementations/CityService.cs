using Domain.POCOs;
using Mapster;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Models;
using Services.Models.ServiceModels;

namespace Services.Implementations;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;

    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }


    public async Task<CityServiceModel> GetCityByNameAsync(string name)
    {
        var obj = await _cityRepository.GetCityByNameAsync(name);
        return obj.Adapt<CityServiceModel>();
    }

    public async Task<List<CityServiceModel>> GetAllCitiesAsync()
    {
        var entities = await _cityRepository.GetAllCitiesAsync();
        return entities.Adapt<List<CityServiceModel>>();
    }

}