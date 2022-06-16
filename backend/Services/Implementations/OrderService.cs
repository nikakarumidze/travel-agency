using Mapster;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Exceptions;
using Services.Localisations;
using Services.Models.ServiceModels;

namespace Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderProcessor _orderProcessor;
    
    
    public OrderService(IOrderRepository orderRepository, IOrderProcessor orderProcessor)
    {
        _orderRepository = orderRepository;
        _orderProcessor = orderProcessor;
    }

    public async Task<OrderServiceModel> GetAsync(int id)
    {
        var obj = await _orderRepository.GetAsync(id);
        if (obj is null)
            throw new NotFoundException(ExceptionMessages.ObjectNotFound);
        
        return obj.Adapt<OrderServiceModel>();
    }

    public async Task<List<OrderServiceModel>> GetWhereIHostAsync(string username)
    {
        var entities = await _orderRepository.GetWhereIHostAsync(username);
        
        return entities.Adapt<List<OrderServiceModel>>();
    }

    public async Task<List<OrderServiceModel>> GetWhereITravelAsync(string username)
    {
        var entities = await _orderRepository.GetWhereITravelAsync(username);
        return entities.Adapt<List<OrderServiceModel>>();
    }

    public async Task<List<OrderServiceModel>> GetPendingWhereIHostAsync(string username)
    {
        var entities = await _orderRepository.GetPendingWhereIHostAsync(username);
        return entities.Adapt<List<OrderServiceModel>>();
    }

    public async Task<List<OrderServiceModel>> GetPendingWhereITravelAsync(string username)
    {
        var entities = await _orderRepository.GetWhereITravelAsync(username);
        return entities.Adapt<List<OrderServiceModel>>();
    }

    public async Task<int> ProcessABooking(OrderServiceModel order)
    {
        var id = await _orderProcessor.BookAnApartment(order);
        return id;
    }

    public async Task ChangeOrderStatusAsync(int orderId, bool accepted)
    {
        await _orderProcessor.ChangeBookingStatusAsync(orderId, accepted);
    }

    public async Task DeleteAsync(int id)
    {
        await _orderRepository.DeleteAsync(id);
    }
}