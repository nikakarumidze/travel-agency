using System.Net;
using API.Contracts.V1;
using API.Models.UserRequests.OrderRequestModels;
using Domain.POCOs;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Models.ServiceModels;

namespace API.Controllers;

[ApiController]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrderController(IOrderService orderService, UserManager<ApplicationUser> userManager)
    {
        _orderService = orderService;
        _userManager = userManager;
    }

    [Authorize]
    [HttpPost(ApiRoutes.Order.Book)]
    public async Task<IActionResult> BookAsync([FromBody] OrderBookingRequestModel requestModel)
    {
        var username = HttpContext.User.Identity.Name;
        var user = await _userManager.FindByNameAsync(username);
        var adapted = requestModel.Adapt<OrderServiceModel>();
        adapted.GuestId = user.Id;
        var id = await _orderService.ProcessABooking(adapted);
        return Ok(id);
    }

    [Authorize]
    [HttpGet(ApiRoutes.Order.GetWhereIHost)]
    public async Task<IActionResult> GetWhereIHost()
    {
        var userName = HttpContext.User.Identity.Name;
        var entities = await _orderService.GetWhereIHostAsync(userName);
        return Ok(entities);
    }
    
    [Authorize]
    [HttpGet(ApiRoutes.Order.GetWhereITravel)]
    public async Task<IActionResult> GetWhereITravel()
    {
        var userName = HttpContext.User.Identity.Name;
        var entities = await _orderService.GetWhereITravelAsync(userName);
        return Ok(entities);
    }
    
    [Authorize]
    [HttpGet(ApiRoutes.Order.GetPendingWhereIHost)]
    public async Task<IActionResult> GetPendingWhereIHost()
    {
        var userName = HttpContext.User.Identity.Name;
        var entities = await _orderService.GetPendingWhereIHostAsync(userName);
        return Ok(entities);
    }

    [Authorize]
    [HttpGet(ApiRoutes.Order.GetPendingWhereITravel)]
    public async Task<IActionResult> GetPendingWhereITravel()
    {
        var userName = HttpContext.User.Identity.Name;
        var entities = await _orderService.GetPendingWhereITravelAsync(userName);
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