using Services.Models;
using Services.Models.ServiceModels;

namespace Services.Abstractions;

public interface IOrderService
{
    Task<OrderServiceModel> GetAsync(int id);
    Task<List<OrderServiceModel>> GetWhereIHostAsync();
    Task<List<OrderServiceModel>> GetWhereITravelAsync();
    Task<List<OrderServiceModel>> GetPendingWhereIHostAsync();
    Task<List<OrderServiceModel>> GetPendingWhereITravelAsync();
    Task<int> ProcessABooking(OrderServiceModel order);
    Task ChangeOrderStatusAsync(int orderId, bool accepted);
    Task DeleteAsync(int id);
}