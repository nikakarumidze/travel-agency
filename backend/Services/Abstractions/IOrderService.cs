using Services.Models;
using Services.Models.ServiceModels;

namespace Services.Abstractions;

public interface IOrderService
{
    Task<OrderServiceModel> GetAsync(int id);
    Task<List<OrderServiceModel>> GetWhereIHostAsync(string username);
    Task<List<OrderServiceModel>> GetWhereITravelAsync(string username);
    Task<List<OrderServiceModel>> GetPendingWhereIHostAsync(string username);
    Task<List<OrderServiceModel>> GetPendingWhereITravelAsync(string username);
    Task<int> ProcessABooking(OrderServiceModel order);
    Task ChangeOrderStatusAsync(int orderId, bool accepted);
    Task DeleteAsync(int id);
}