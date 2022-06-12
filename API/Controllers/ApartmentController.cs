using API.Contracts.V1;
using API.Models.UserRequests;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Models;

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