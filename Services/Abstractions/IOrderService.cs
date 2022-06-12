using Services.Models;

namespace Services.Abstractions;

public interface IOrderService
{
    Task<OrderServiceModel> GetAsync(int id);
    Task<List<OrderServiceModel>> GetWhereIHostAsync(string userId);
    Task<List<OrderServiceModel>> GetWhereITravelAsync(string userId);
    Task<List<OrderServiceModel>> GetPendingWhereIHostAsync(string userId);
    Task<List<OrderServiceModel>> GetPendingWhereITravelAsync(string userId);
    Task<int> CreateAsync(OrderServiceModel order);
    Task ChangeOrderStatusAsync(int orderId, bool accepted);
    Task DeleteAsync(int id);
}