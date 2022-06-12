using Domain.POCOs;
using Mapster;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Models;

namespace Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderServiceModel> GetAsync(int id)
    {
        var obj = await _orderRepository.GetAsync(id);
        return obj.Adapt<OrderServiceModel>();
    }

    public async Task<List<OrderServiceModel>> GetWhereIHostAsync(string userId)
    {
        var entities = await _orderRepository.GetWhereIHostAsync(userId);
        return entities.Adapt<List<OrderServiceModel>>();
    }

    public async Task<List<OrderServiceModel>> GetWhereITravelAsync(string userId)
    {
        var entities = await _orderRepository.GetWhereITravelAsync(userId);
        return entities.Adapt<List<OrderServiceModel>>();
    }

    public async Task<List<OrderServiceModel>> GetPendingWhereIHostAsync(string userId)
    {
        var entities = await _orderRepository.GetPendingWhereIHostAsync(userId);
        return entities.Adapt<List<OrderServiceModel>>();
    }

    public async Task<List<OrderServiceModel>> GetPendingWhereITravelAsync(string userId)
    {
        var entities = await _orderRepository.GetWhereITravelAsync(userId);
        return entities.Adapt<List<OrderServiceModel>>();
    }

    public async Task<int> CreateAsync(OrderServiceModel order)
    {
        var id = await _orderRepository.CreateAsync(order.Adapt<Order>());
        return id;
    }

    public async Task ChangeOrderStatusAsync(int orderId, bool accepted)
    {
        await _orderRepository.ChangeOrderStatusAsync(orderId, accepted);
    }

    public async Task DeleteAsync(int id)
    {
        await _orderRepository.DeleteAsync(id);
    }
}