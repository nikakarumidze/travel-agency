using Domain.POCOs;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;
using Repositories.Models;

namespace Repositories.Implementations;

public class ApartmentRepository : IApartmentRepository
{
    private readonly IBaseRepository<Apartment> _baseRepository;
    private readonly IOrderRepository _orderRepository;
    
    
    public ApartmentRepository(IBaseRepository<Apartment> baseRepository, IOrderRepository orderRepository)
    {
        _baseRepository = baseRepository;
        _orderRepository = orderRepository;
    }
    
    public async Task<Apartment> GetAsync(int id)
    {
        return await _baseRepository.GetAsync(id);
    }

    public async Task<Apartment> GetByOwnerIdAsync(string id)
    {
        return await _baseRepository.Table
            .Include(x=>x.City)
            .SingleOrDefaultAsync(x => x.OwnerId == id);
    }

    public async Task<List<Apartment>> GetAllAsync()
    {
        return await _baseRepository.Table
            .Include(x=>x.Owner)
            .ToListAsync();
    }

    public async Task<List<Apartment>> GetAllByCityAsync(string city)
    {
        var objs = await _baseRepository.Table
            .Include(x=>x.Owner)
            .Include(x=>x.City)
            .Where(x => x.City.Name == city)
            .ToListAsync();
        
        return objs;
    }

    public async Task<List<Apartment>> GetByAddressAsync(string address)
    {
        return await _baseRepository.Table
            .Include(x=>x.Owner)
            .Include(x=>x.City)
            .Where(x => x.Address.Contains(address))
            .ToListAsync();
    }

    public async Task<List<Apartment>> GetAllWithOwnersAsync()
    {
        return await _baseRepository.Table
            .Include(x => x.Owner)
            .Include(x=>x.City)
            .ToListAsync();
    }

    public async Task<Apartment> GetWithOwnerAsync(int id)
    {
        return await _baseRepository.Table
            .Include(x => x.Owner)
            .Include(x=>x.City)
            .SingleOrDefaultAsync(x => x.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task<int> CreateAsync(Apartment apartment)
    {
        var obj = await _baseRepository.CreateAsync(apartment);
        return obj.Id;
    }

    public async Task UpdateAsync(Apartment apartment)
    {
        await _baseRepository.UpdateAsync(apartment);
    }

    public async Task DeleteAsync(int id)
    {
        var obj = await GetAsync(id);
        await _baseRepository.DeleteAsync(obj);
    }

    public async Task<List<Apartment>> SearchAsync(ApartmentSearchModel model)
    {
        var apartments = _baseRepository.Table.AsQueryable();
        if (model.BedsNumber != null)
            apartments = apartments.Where(x => x.BedsNumber == model.BedsNumber);
        if (model.City is not null)
            apartments = apartments.Where(x => x.City.Name.Contains(model.City));
        if (model.Wifi is not null)
            apartments = apartments.Where(x => x.Wifi == model.Wifi);
        if (model.Conditioner is not null)
            apartments = apartments.Where(x => x.Conditioner == model.Conditioner);
        if (model.Parking is not null)
            apartments = apartments.Where(x => x.Parking == model.Parking);
        if (model.Pool is not null)
            apartments = apartments.Where(x => x.Pool == model.Pool);
        if (model.Gym is not null)
            apartments = apartments.Where(x => x.Gym == model.Gym);

        return await apartments.ToListAsync();
    }
}