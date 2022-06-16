using API.Contracts.V1;
using API.Models.UserRequests.OrderRequestModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Models.ServiceModels;

namespace API.Controllers;

[ApiController]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [Authorize]
    [HttpPost(ApiRoutes.Order.Book)]
    public async Task<IActionResult> BookAsync([FromBody] OrderBookingRequestModel requestModel)
    {
        var id = await _orderService.ProcessABooking(requestModel.Adapt<OrderServiceModel>());
        return Ok(id);
    }

    [Authorize]
    [HttpGet(ApiRoutes.Order.GetWhereIHost)]
    public async Task<IActionResult> GetWhereIHost(string userId)
    {
        var entities = await _orderService.GetWhereIHostAsync(userId);
        return Ok(entities);
    }
    
    [Authorize]
    [HttpGet(ApiRoutes.Order.GetWhereITravel)]
    public async Task<IActionResult> GetWhereITravel(string userId)
    {
        var entities = await _orderService.GetWhereITravelAsync(userId);
        return Ok(entities);
    }
    
    [Authorize]
    [HttpGet(ApiRoutes.Order.GetPendingWhereIHost)]
    public async Task<IActionResult> GetPendingWhereIHost(string userId)
    {
        var entities = await _orderService.GetPendingWhereIHostAsync(userId);
        return Ok(entities);
    }

    [Authorize]
    [HttpGet(ApiRoutes.Order.GetPendingWhereITravel)]
    public async Task<IActionResult> GetPendingWhereITravel(string userId)
    {
        var entities = await _orderService.GetPendingWhereITravelAsync(userId);
        return Ok(entities);
    }

    [Authorize]
    [HttpPut(ApiRoutes.Order.ChangeOrderStatus)]
    public async Task<IActionResult> ChangeOrderStatus(int orderId, bool approved)
    {
        await _orderService.ChangeOrderStatusAsync(orderId, approved);
        return Ok();
    }
}