using API.Contracts.V1;
using API.Models.UserRequests.ApartmentRequestModels;
using Domain.POCOs;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Models.ServiceModels;

namespace API.Controllers;

[ApiController]
public class ApartmentController : Controller
{
    private readonly IApartmentService _apartmentService;
    private readonly UserManager<ApplicationUser> _userManager;


    public ApartmentController(IApartmentService apartmentService, UserManager<ApplicationUser> userManager)
    {
        _apartmentService = apartmentService;
        _userManager = userManager;
    }
    [AllowAnonymous]
    [HttpGet(ApiRoutes.Apartment.GetAll)]
    public async Task<IActionResult> GetAllAsync()
    {
        var objs = await _apartmentService.GetAllAsync();
        return Ok(objs);
    }
    
    [AllowAnonymous]
    [HttpGet(ApiRoutes.Apartment.GetAllWithBusyDates)]
    public async Task<IActionResult> GetAllWithBusyDatesAsync()
    {
        var objs = await _apartmentService.GetAllWithBusyDatesAsync();
        return Ok(objs);
    }
    
    [AllowAnonymous]
    [HttpGet(ApiRoutes.Apartment.GetWithBusyDates)]
    public async Task<IActionResult> GetWithBusyDatesAsync(int apartmentId)
    {
        var objs = await _apartmentService.GetWithBusyDatesAsync(apartmentId);
        return Ok(objs);
    }
    
    
    /// <summary>
    /// Only fill in the fields the user wants to search, leave others null
    /// </summary>
    [AllowAnonymous]
    [HttpGet(ApiRoutes.Apartment.Search)]
    public async Task<IActionResult> Search([FromBody]SearchApartmentsRequestModel model)
    {
        var objs = await _apartmentService
            .Search(model.Adapt<ApartmentSearchServiceModel>());
        return Ok(objs);
    }


    [AllowAnonymous]
    [HttpGet(ApiRoutes.Apartment.GetAllByCity)]
    public async Task<IActionResult> GetAllByCity(string city)
    {
        var objs = await _apartmentService.GetAllByCityAsync(city);
        return Ok(objs);
    }
    
    
    [AllowAnonymous]
    [HttpGet(ApiRoutes.Apartment.GetAllByAddress)]
    public async Task<IActionResult> GetAllByAddress(string address)
    {
        var objs = await _apartmentService.GetAllByAddressAsync(address);
        return Ok(objs);
    }
    
    [Authorize]
    [HttpGet(ApiRoutes.Apartment.GetMine)]
    public async Task<IActionResult> GetMyApartmentAsync()
    {
        var username = HttpContext.User.Identity.Name;
        var obj = await _apartmentService.GetMineAsync(username);
        return Ok(obj);
    }

    [Authorize]
    [HttpPost(ApiRoutes.Apartment.Create)]
    public async Task<IActionResult> CreateAsync([FromBody]CreateApartmentRequestModel request)
    {
        var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
        var adapted = request.Adapt<ApartmentServiceModel>();
        adapted.OwnerId = user.Id;
        var obj = await _apartmentService
            .CreateMineAsync(adapted);
        return Ok(obj);
    }

    [Authorize]
    [HttpPut(ApiRoutes.Apartment.Update)]
    public async Task<IActionResult> UpdateAsync([FromBody]UpdateApartmentRequestModel request)
    {
        var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
        var adapted = request.Adapt<ApartmentServiceModel>();
        adapted.OwnerId = user.Id;
        await _apartmentService
            .UpdateMineAsync(adapted);
        return Ok();
    }

    [Authorize]
    [HttpDelete(ApiRoutes.Apartment.Delete)]
    public async Task<IActionResult> DeleteAsync()
    {
        var username = HttpContext.User.Identity.Name;
        await _apartmentService.DeleteMineAsync(username);
        return Ok();
    }
}