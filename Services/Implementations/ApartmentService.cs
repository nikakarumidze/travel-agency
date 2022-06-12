using Domain.POCOs;
using Mapster;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Exceptions;
using Services.Localisations;
using Services.Models;

namespace Services.Implementations;

public class ApartmentService : IApartmentService
{
    private readonly IApartmentRepository _apartmentRepository;
    private readonly ICityRepository _cityRepository;
    
    
    public ApartmentService(IApartmentRepository apartmentRepository, ICityRepository cityRepository)
    {
        _apartmentRepository = apartmentRepository;
        _cityRepository = cityRepository;
    }

    public async Task<List<ApartmentServiceModel>> GetAllAsync()
    {
        var objs = await _apartmentRepository.GetAllAsync();
        return objs.Adapt<List<ApartmentServiceModel>>();
    }

    public async Task<List<ApartmentServiceModel>> GetAllByCityAsync(string city)
    {
        var objs = await _apartmentRepository.GetAllByCityAsync(city);
        return objs.Adapt<List<ApartmentServiceModel>>();
    }

    public async Task<List<ApartmentServiceModel>> GetAllByAddressAsync(string address)
    {
        var objs = await _apartmentRepository.GetByAddressAsync(address);
        return objs.Adapt<List<ApartmentServiceModel>>();
    }

    public async Task<ApartmentServiceModel> GetMineAsync(string id)
    {
        var obj = await _apartmentRepository.GetByOwnerIdAsync(id);
        if (obj == null)
            throw new NotFoundException(ExceptionMessages.ObjectNotFound);
        return obj.Adapt<ApartmentServiceModel>();
    }

    public async Task<int> CreateMineAsync(ApartmentServiceModel request)
    {
        var obj = await _apartmentRepository.GetByOwnerIdAsync(request.OwnerId);
        if (obj is not null)
            throw new ObjectAlreadyExistsException(ExceptionMessages.ObjectAlreadyExists);
        
        
        var cityObj = await _cityRepository.GetCityByNameAsync(request.CityName);
        if (cityObj is null)
            throw new NotFoundException(ExceptionMessages.CityNotFound);
        
        request.CityId = cityObj.Id;

        return await _apartmentRepository
            .CreateAsync(request.Adapt<Apartment>());
    }

    public async Task UpdateMineAsync(ApartmentServiceModel request)
    {
        var obj = await _apartmentRepository
            .GetByOwnerIdAsync(request.OwnerId);


        var cityObj = await _cityRepository.GetCityByNameAsync(request.CityName);
        if (cityObj is null)
            throw new NotFoundException(ExceptionMessages.CityNotFound);
        obj.City = cityObj.Adapt<City>();

        obj.Address = request.Address;
        obj.Conditioner = request.Conditioner;
        obj.Gym = request.Gym;
        obj.Image = request.Image;
        obj.Parking = request.Parking;
        obj.Pool = request.Pool;
        obj.Wifi = request.Wifi;
        obj.BedsNumber = request.BedsNumber;
        obj.DistanceToCenter = request.DistanceToCenter;
        
        await _apartmentRepository.UpdateAsync(obj);
    }

    public async Task DeleteMineAsync(string userId)
    {
        var obj = await _apartmentRepository.GetByOwnerIdAsync(userId);
        if (obj is null)
            throw new NotFoundException(ExceptionMessages.ObjectNotFound);
        
        await _apartmentRepository.DeleteAsync(obj.Id);
    }
}