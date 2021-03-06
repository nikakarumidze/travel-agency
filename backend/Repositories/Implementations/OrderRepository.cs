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

    public async Task<List<Order>> GetWhereITravelAsync(string username)
    {
        return await _baseRepository.Table
            .Include(x=>x.Host)
            .Include(x => x.Host.Apartment)
            .Where(x => x.Guest.UserName == username && x.Approved == true)
            .ToListAsync();
    }

    public async Task<List<Order>> GetWhereIHostAsync(string username)
    {
        return await _baseRepository.Table
            .Include(x => x.Host.Apartment)
            .Include(x=>x.Guest)
            .Where(x => x.Host.UserName == username && x.Approved == true)
            .ToListAsync();
    }

    public async Task<List<Order>> GetPendingWhereITravelAsync(string username)
    {
        return await _baseRepository.Table
            .Include(x => x.Host)
            .Where(x => x.Approved == null && x.Guest.UserName == username)
            .ToListAsync();
    }

    public async Task<List<Order>> GetPendingWhereIHostAsync(string username)
    {
        var entities = await _baseRepository.Table
            .Include(x => x.Guest)
            .Where(x => x.Approved == null && x.Host.UserName == username)
            .ToListAsync();
        return entities;
    }

    public async Task<List<Order>> GetActiveOrdersForApartment(int apartmentId)
    {
        var entities = await _baseRepository.Table
            .Where(x => x.Host.Apartment.Id == apartmentId && x.Approved == true)
            .ToListAsync();
        return entities;
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