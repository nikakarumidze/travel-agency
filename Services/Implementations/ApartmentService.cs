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

    public ApartmentService(IApartmentRepository apartmentRepository)
    {
        _apartmentRepository = apartmentRepository;
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

    public async Task<ApartmentServiceModel> GetMyApartmentAsync(string id)
    {
        var obj = await _apartmentRepository.GetByOwnerIdAsync(id);
        if (obj == null)
            throw new NotFoundException(ExceptionMessages.ObjectNotFound);
        return obj.Adapt<ApartmentServiceModel>();
    }

    public async Task<int> CreateApartmentAsync(ApartmentServiceModel request)
    {
        var obj = await _apartmentRepository.GetByOwnerIdAsync(request.OwnerId);
        if (obj is not null)
            throw new ObjectAlreadyExistsException(ExceptionMessages.ObjectAlreadyExists);
        return await _apartmentRepository
            .CreateAsync(request.Adapt<Apartment>());
    }

    public async Task UpdateApartmentAsync(ApartmentServiceModel request)
    {
        var obj = await _apartmentRepository
            .GetByOwnerIdAsync(request.OwnerId);
        
        obj.Address = request.Address;
        obj.City = request.City;
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
}