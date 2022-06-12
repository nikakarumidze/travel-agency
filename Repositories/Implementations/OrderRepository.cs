using Domain.POCOs;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;

namespace Repositories.Implementations;

public class OrderRepository : IOrderRepository
{
    private readonly IBaseRepository<Order> _baseRepository;

    public OrderRepository(IBaseRepository<Order> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public async Task<Order> GetAsync(int id)
    {
        return await _baseRepository.GetAsync(id);
    }

    public async Task<List<Order>> GetWhereITravelAsync(string id)
    {
        return await _baseRepository.Table
            .Include(x=>x.Host)
            .Include(x => x.Host.Apartment)
            .Where(x => x.GuestId == id)
            .ToListAsync();
    }

    public async Task<List<Order>> GetWhereIHostAsync(string id)
    {
        return await _baseRepository.Table
            .Include(x => x.Host.Apartment)
            .Include(x=>x.Guest)
            .Where(x => x.HostId == id)
            .ToListAsync();
    }

    public async Task<List<Order>> GetPendingWhereITravelAsync(string userId)
    {
        return await _baseRepository.Table
            .Include(x => x.Host)
            .Where(x => x.Approved == null && x.GuestId == userId)
            .ToListAsync();
    }

    public async Task<List<Order>> GetPendingWhereIHostAsync(string userId)
    {
        return await _baseRepository.Table
            .Include(x => x.Guest)
            .Where(x => x.Approved == null && x.HostId == userId)
            .ToListAsync();
    }

    public async Task<Apartment> GetHostApartment(int id)
    {
        var order = await _baseRepository.Table
            .Include(x=>x.Host.Apartment)
            .SingleOrDefaultAsync(x => x.Id == id);
        return order.Host.Apartment;
    }

    public async Task<int> CreateAsync(Order order)
    {
        var obj = await _baseRepository.CreateAsync(order);
        return obj.Id;
    }

    public async Task ChangeOrderStatusAsync(int orderId, bool accepted)
    {
        var order = await GetAsync(orderId);
        order.Approved = accepted;
        await _baseRepository.UpdateAsync(order);
    }

    public async Task DeleteAsync(int id)
    {
        var obj = await GetAsync(id);
        await _baseRepository.SetIsDeletedAsync(obj);
    }
}