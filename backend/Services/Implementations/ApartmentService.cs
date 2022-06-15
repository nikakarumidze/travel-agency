using Domain.POCOs;
using Mapster;
using Repositories.Abstractions;
using Repositories.Models;
using Services.Abstractions;
using Services.Exceptions;
using Services.Localisations;
using Services.Models;
using Services.Models.ServiceModels;

namespace Services.Implementations;

public class ApartmentService : IApartmentService
{
    private readonly IApartmentRepository _apartmentRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IOrderRepository _orderRepository;
    
    public ApartmentService(IApartmentRepository apartmentRepository, 
        ICityRepository cityRepository, IOrderRepository orderRepository)
    {
        _apartmentRepository = apartmentRepository;
        _cityRepository = cityRepository;
        _orderRepository = orderRepository;
    }

    
    #region Methods
    public async Task<List<ApartmentServiceModel>> GetAllAsync()
    {
        var objs = await _apartmentRepository.GetAllAsync();
        
        return objs.Adapt<List<ApartmentServiceModel>>();
    }

    public async Task<List<ApartmentServiceModel>> GetAllWithBusyDatesAsync()
    {
        var objs = await _apartmentRepository.GetAllAsync();
        var adapted = objs.Adapt<List<ApartmentServiceModel>>();
        
        foreach (var item in adapted)
        {
            item.BusyDates = await GetBusyDates(item.Id);
        }

        return adapted;
    }

    public async Task<ApartmentServiceModel> GetWithBusyDatesAsync(int apartmentId)
    {
        var obj = await _apartmentRepository.GetAsync(apartmentId);
        var adapted = obj.Adapt<ApartmentServiceModel>();
        
        adapted.BusyDates = await GetBusyDates(adapted.Id);

        return adapted;
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
        await FillApartment(obj, request);
        await _apartmentRepository.UpdateAsync(obj);
    }

    public async Task DeleteMineAsync(string userId)
    {
        var obj = await _apartmentRepository.GetByOwnerIdAsync(userId);
        if (obj is null)
            throw new NotFoundException(ExceptionMessages.ObjectNotFound);
        
        await _apartmentRepository.DeleteAsync(obj.Id);
    }
    
    public async Task<List<ApartmentServiceModel>> Search(ApartmentSearchServiceModel search)
    {
        var objs = await _apartmentRepository
            .SearchAsync(search.Adapt<ApartmentSearchModel>());

        var adapted = objs.Adapt<List<ApartmentServiceModel>>();

        if (search.AvailableFrom is null || search.AvailableTo is null) return adapted;
        
        foreach (var item in adapted.ToList())
        {
            var busyDates = await GetBusyDates(item.Id);
            var requestDates = AllDaysFromRange(search.AvailableFrom.Value,
                search.AvailableTo.Value);

            item.BusyDates = busyDates;
            foreach (var itemm in requestDates)
            {
                if (busyDates.Contains(itemm))
                {
                    adapted.Remove(item);
                    break;
                }
            }
        }

        return adapted;
    }
    
    #endregion
    
    #region Private Methods

    private async Task<Apartment> FillApartment(Apartment obj, ApartmentServiceModel request)
    {
        obj.Address = request.Address;
        obj.Conditioner = request.Conditioner;
        obj.Gym = request.Gym;
        obj.Image = request.Image;
        obj.Parking = request.Parking;
        obj.Pool = request.Pool;
        obj.Wifi = request.Wifi;
        obj.BedsNumber = request.BedsNumber;
        obj.DistanceToCenter = request.DistanceToCenter;

        return obj;
    }
    
    private async Task<List<DateTime>> GetBusyDates(int Id)
    {
        var dates = new List<DateTime>();
        var entities = await _orderRepository.GetActiveOrdersForApartment(Id);
        foreach (var entity in entities)
        {
            dates.AddRange(AllDaysFromRange(entity.From, entity.To));
        }

        return dates;
    }
    private static IEnumerable<DateTime> AllDaysFromRange(DateTime from, DateTime to)
    {
        var list = new List<DateTime>();
        for (var dt = from; dt <= to; dt = dt.AddDays(1))
        {
            list.Add(dt);
        }

        return list; 
    }
    #endregion
}