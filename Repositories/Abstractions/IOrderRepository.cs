using Domain.POCOs;

namespace Repositories.Abstractions;

public interface IOrderRepository
{
    Task<Order> GetAsync(int id);
    Task<List<Order>> GetWhereITravelAsync(string userId);
    Task<List<Order>> GetWhereIHostAsync(string userId);
    Task<List<Order>> GetPendingWhereITravelAsync(string userId);
    Task<List<Order>> GetPendingWhereIHostAsync(string userId);
    Task<Apartment> GetHostApartment(int id);
    Task<int> CreateAsync(Order order);
    Task ChangeOrderStatusAsync(int orderId, bool accepted);
    Task DeleteAsync(int id);
}