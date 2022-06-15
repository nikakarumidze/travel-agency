using API.Contracts.V1;
using API.Models.UserRequests;
using API.Models.UserRequests.ApartmentRequestModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Models;
using Services.Abstractions;
using Services.Models;
using Services.Models.ServiceModels;

namespace API.Controllers;

public class ApartmentController : Controller
{
    private readonly IApartmentService _apartmentService;

    public ApartmentController(IApartmentService apartmentService)
    {
        _apartmentService = apartmentService;
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
    public async Task<IActionResult> Search(SearchApartmentsRequestModel model)
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
    public async Task<IActionResult> GetMyApartmentAsync(string id)
    {
        var obj = await _apartmentService.GetMineAsync(id);
        return Ok(obj);
    }

    [Authorize]
    [HttpPost(ApiRoutes.Apartment.Create)]
    public async Task<IActionResult> CreateAsync(CreateApartmentRequestModel request)
    {
        var obj = await _apartmentService
            .CreateMineAsync(request.Adapt<ApartmentServiceModel>());
        return Ok(obj);
    }

    [Authorize]
    [HttpPut(ApiRoutes.Apartment.Update)]
    public async Task<IActionResult> UpdateAsync(UpdateApartmentRequestModel request)
    {
        await _apartmentService
            .UpdateMineAsync(request.Adapt<ApartmentServiceModel>());
        return Ok();
    }

    [Authorize]
    [HttpDelete(ApiRoutes.Apartment.Delete)]
    public async Task<IActionResult> DeleteAsync(string userId)
    {
        await _apartmentService.DeleteMineAsync(userId);
        return Ok();
    }
}