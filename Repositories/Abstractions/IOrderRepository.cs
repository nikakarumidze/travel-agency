using Domain.POCOs;

namespace Repositories.Abstractions;

public interface IOrderRepository
{
    Task<Order> GetAsync(int id);
    Task<List<Order>> GetWhereITravelAsync(string id);
    Task<List<Order>> GetWhereIHostAsync(string id);
    Task<Apartment> GetHostApartment(int id);
    Task<int> CreateAsync(Order order);
    Task ChangeOrderStatusAsync(int orderId, bool accepted);
    Task DeleteAsync(int id);
}