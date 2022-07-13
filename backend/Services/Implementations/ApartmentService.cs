using Domain;
using Domain.POCOs;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Serialization;
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
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly string _username;
    
    public ApartmentService(IApartmentRepository apartmentRepository, 
        ICityRepository cityRepository, IOrderRepository orderRepository, IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager)
    {
        _apartmentRepository = apartmentRepository;
        _cityRepository = cityRepository;
        _orderRepository = orderRepository;
        _contextAccessor = contextAccessor;
        _userManager = userManager;
        _username = _contextAccessor.HttpContext.User.Identity.Name;
    }

    
    #region Methods

    public async Task<List<ApartmentServiceModel>> GetAllAsync(PaginationFilter pagination)
    {
        var objs = await _apartmentRepository.GetAllAsync(pagination);

        return objs.Adapt<List<ApartmentServiceModel>>();
    }

    public async Task<List<ApartmentServiceModel>> GetAllWithBusyDatesAsync(PaginationFilter pagination)
    {
        var objs = await _apartmentRepository.GetAllAsync(pagination);
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

    public async Task<List<ApartmentServiceModel>> GetAllByCityAsync(string city, PaginationFilter pagination)
    {

        var objs = await _apartmentRepository.GetAllByCityAsync(city, pagination);
        return objs.Adapt<List<ApartmentServiceModel>>();
    }

    public async Task<List<ApartmentServiceModel>> GetAllByAddressAsync(string address)
    {
        var objs = await _apartmentRepository.GetByAddressAsync(address);
        return objs.Adapt<List<ApartmentServiceModel>>();
    }

    public async Task<ApartmentServiceModel> GetMineAsync()
    {
        var obj = await _apartmentRepository.GetByOwnerUsernameAsync(_username);
        if (obj == null)
            throw new NotFoundException(ExceptionMessages.ObjectNotFound);
        return obj.Adapt<ApartmentServiceModel>();
    }

    public async Task<int> CreateMineAsync(ApartmentServiceModel request)
    {
        var user = await _userManager.FindByNameAsync(_username);
        request.OwnerId = user.Id;
        
        var obj = await _apartmentRepository.GetByOwnerIdAsync(request.OwnerId);
        if (obj is not null)
            throw new ObjectAlreadyExistsException(ExceptionMessages.ObjectAlreadyExists);
        
        var cityObj = await _cityRepository.GetCityByNameAsync(request.CityName);
        if (cityObj is null)
        {
            cityObj = await _cityRepository.CreateCityAsync(new City{Name=request.CityName});
        }

        request.CityId = cityObj.Id;

        var adapted = request.Adapt<Apartment>();

        adapted.Image = Convert.FromBase64String(request.ImageAsBase64);
        
        return await _apartmentRepository
            .CreateAsync(request.Adapt<Apartment>());
    }

    public async Task UpdateMineAsync(ApartmentServiceModel request)
    {
        var user = await _userManager.FindByNameAsync(_username);
        request.OwnerId = user.Id;

        var obj = await _apartmentRepository.GetByOwnerIdAsync(request.OwnerId);

        var cityObj = await _cityRepository.GetCityByNameAsync(request.CityName);
        if (cityObj is null)
            throw new NotFoundException(ExceptionMessages.CityNotFound);

        if (obj.City.Name != request.CityName)
            obj.City = cityObj;
        await FillApartment(obj, request);
        
        await _apartmentRepository.UpdateAsync(obj);
    }

    public async Task DeleteMineAsync()
    {
        var obj = await _apartmentRepository.GetByOwnerUsernameAsync(_username);
        if (obj is null)
            throw new NotFoundException(ExceptionMessages.ObjectNotFound);
        
        await _apartmentRepository.DeleteAsync(obj.Id);
    }
    
    public async Task<List<ApartmentServiceModel>> Search(ApartmentSearchServiceModel search, PaginationFilter pagination)
    {
        var objs = await _apartmentRepository
            .SearchAsync(search.Adapt<ApartmentSearchModel>(), pagination);

        var adapted = objs.Adapt<List<ApartmentServiceModel>>();

        if (search.AvailableFrom is null || search.AvailableTo is null) 
            return adapted;
        
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
        obj.MaxGuest = request.MaxGuest;
        obj.DistanceToCenter = request.DistanceToCenter;
        obj.Description = request.Description;
        obj.Image = Convert.FromBase64String(request.ImageAsBase64);
        
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