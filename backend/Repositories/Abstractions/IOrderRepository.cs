using Domain.POCOs;

namespace Repositories.Abstractions;

public interface IOrderRepository
{
    Task<Order> GetAsync(int id);
    Task<List<Order>> GetWhereITravelAsync(string username);
    Task<List<Order>> GetWhereIHostAsync(string username);
    Task<List<Order>> GetPendingWhereITravelAsync(string username);
    Task<List<Order>> GetPendingWhereIHostAsync(string username);
    Task<List<Order>> GetActiveOrdersForApartment(int apartmentId);
    Task<int> CreateAsync(Order order);
    Task ChangeOrderStatusAsync(int orderId, bool accepted);
    Task DeleteAsync(int id);
}