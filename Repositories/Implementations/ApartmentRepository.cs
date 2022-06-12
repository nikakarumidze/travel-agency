using Domain.POCOs;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;

namespace Repositories.Implementations;

public class ApartmentRepository : IApartmentRepository
{
    private readonly IBaseRepository<Apartment> _baseRepository;

    public ApartmentRepository(IBaseRepository<Apartment> baseRepository)
    {
        _baseRepository = baseRepository;
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
}