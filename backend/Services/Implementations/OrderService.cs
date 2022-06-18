using Domain.POCOs;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    private readonly IHttpContextAccessor _httpContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly string _username;
    
    
    public OrderService(IOrderRepository orderRepository, IOrderProcessor orderProcessor,
        IHttpContextAccessor httpContext, UserManager<ApplicationUser> userManager)
    {
        _orderRepository = orderRepository;
        _orderProcessor = orderProcessor;
        _httpContext = httpContext;
        _userManager = userManager;
        _username = _httpContext.HttpContext.User.Identity.Name;
    }

    public async Task<OrderServiceModel> GetAsync(int id)
    {
        var obj = await _orderRepository.GetAsync(id);
        
        if (obj is null)
            throw new NotFoundException(ExceptionMessages.ObjectNotFound);
        
        return obj.Adapt<OrderServiceModel>();
    }

    public async Task<List<OrderServiceModel>> GetWhereIHostAsync()
    {
        var entities = await _orderRepository.GetWhereIHostAsync(_username);
        
        return entities.Adapt<List<OrderServiceModel>>();
    }

    public async Task<List<OrderServiceModel>> GetWhereITravelAsync()
    {
        var entities = await _orderRepository.GetWhereITravelAsync(_username);
        return entities.Adapt<List<OrderServiceModel>>();
    }

    public async Task<List<OrderServiceModel>> GetPendingWhereIHostAsync()
    {
        var entities = await _orderRepository.GetPendingWhereIHostAsync(_username);
        return entities.Adapt<List<OrderServiceModel>>();
    }

    public async Task<List<OrderServiceModel>> GetPendingWhereITravelAsync()
    {
        var entities = await _orderRepository.GetPendingWhereITravelAsync(_username);
        return entities.Adapt<List<OrderServiceModel>>();
    }

    public async Task<int> ProcessABooking(OrderServiceModel order)
    {
        var user = await _userManager.FindByNameAsync(_username);
        order.GuestId = user.Id;
        
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