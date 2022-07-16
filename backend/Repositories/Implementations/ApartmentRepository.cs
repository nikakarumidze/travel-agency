using Domain;
using Domain.POCOs;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;
using Repositories.Models;

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

    public async Task<Apartment> GetByOwnerUsernameAsync(string username)
    {
        return await _baseRepository.Table
            .Include(x=>x.City)
            .SingleOrDefaultAsync(x => x.Owner.UserName == username);
    }

    public async Task<Apartment> GetByOwnerIdAsync(string id)
    {
        return await _baseRepository.Table
            .Include(x=>x.City)
            .SingleOrDefaultAsync(x => x.OwnerId == id);
    }

    public async Task<List<Apartment>> GetAllAsync(PaginationFilter paginationFilter)
    {
        if(paginationFilter == null)
            return await _baseRepository.Table
                .Include(x=>x.Owner)
                .Include(x=>x.City)
                .ToListAsync();

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
        
        return await _baseRepository.Table
            .Include(x=>x.Owner)
            .Include(x=>x.City)
            .Skip(skip)
            .Take(paginationFilter.PageSize)
            .ToListAsync();
    }

    public async Task<List<Apartment>> GetAllByCityAsync(string city, PaginationFilter paginationFilter)
    {
        if(paginationFilter == null)
            return await _baseRepository.Table
            .Include(x=>x.Owner)
            .Include(x=>x.City)
            .Where(x => x.City.Name == city)
            .ToListAsync();;


        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

        var objs = await _baseRepository.Table
            .Include(x=>x.Owner)
            .Include(x=>x.City)
            .Where(x => x.City.Name == city)
            .Skip(skip)
            .Take(paginationFilter.PageSize)
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

    public async Task<List<Apartment>> SearchAsync(ApartmentSearchModel model, PaginationFilter paginationFilter)
    {
       

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
        
        var apartments = _baseRepository.Table.AsQueryable();
        if (model.MaxGuest != null)
            apartments = apartments.Where(x => x.MaxGuest == model.MaxGuest);
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

        if(paginationFilter == null)
            return await apartments.ToListAsync();
        
        return await apartments
            .Skip(skip)
            .Take(paginationFilter.PageSize)
            .ToListAsync();
    }
}