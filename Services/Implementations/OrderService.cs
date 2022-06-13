using Domain.POCOs;
using Mapster;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Exceptions;
using Services.Localisations;
using Services.Models;

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