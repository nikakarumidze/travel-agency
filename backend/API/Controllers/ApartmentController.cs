using API.Contracts.Queries;
using API.Contracts.Responses;
using API.Contracts.V1;
using API.Infrastructure.Helpers;
using API.Models.DTOs;
using API.Models.UserRequests.ApartmentRequestModels;
using Domain;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Models.ServiceModels;

namespace API.Controllers;

[ApiController]
public class ApartmentController : Controller
{
    private readonly IApartmentService _apartmentService;
    private readonly IUriService _uriService;

    public ApartmentController(IApartmentService apartmentService, IUriService uriService)
    {
        _apartmentService = apartmentService;
        _uriService = uriService;
    }
    [AllowAnonymous]
    [HttpGet(ApiRoutes.Apartment.GetAll)]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationQuery pagination)
    {
        var objs = await _apartmentService
            .GetAllAsync(pagination.Adapt<PaginationFilter>());
        
        var adapted = objs.Adapt<List<ApartmentDTO>>();
        for (var index = 0; index < objs.Count; index++)
        {
            var item = objs[index];
            if (item.Image != null)
            {
                adapted[index].ImageBase64 = Convert.ToBase64String(item.Image);
            }
        }
        
        if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            return Ok(new PagedResponse<ApartmentDTO>(adapted));

        var paginationResponse = PaginationHelpers.CreatePaginatedResponse(_uriService, pagination, adapted);
        return Ok(paginationResponse);
    }
    
    [AllowAnonymous]
    [HttpGet(ApiRoutes.Apartment.GetAllWithBusyDates)]
    public async Task<IActionResult> GetAllWithBusyDatesAsync([FromQuery] PaginationQuery pagination)
    {
        var objs = await _apartmentService
            .GetAllWithBusyDatesAsync(pagination.Adapt<PaginationFilter>());
        
        if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
        return Ok(new PagedResponse<ApartmentServiceModel>(objs));

        var paginationResponse = PaginationHelpers.CreatePaginatedResponse(_uriService, pagination, objs);
        return Ok(paginationResponse);
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
    public async Task<IActionResult> Search([FromBody]SearchApartmentsRequestModel model, 
        [FromQuery] PaginationQuery pagination)
    {
        var objs = await _apartmentService
            .Search(model.Adapt<ApartmentSearchServiceModel>(), pagination.Adapt<PaginationFilter>());
        
        var adapted = objs.Adapt<List<ApartmentDTO>>();
        for (var index = 0; index < objs.Count; index++)
        {
            var item = objs[index];
            if (item.Image != null)
            {
                adapted[index].ImageBase64 = Convert.ToBase64String(item.Image);
            }
        }
        
        if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            return Ok(new PagedResponse<ApartmentDTO>(adapted));

        var paginationResponse = PaginationHelpers.CreatePaginatedResponse(_uriService, pagination, adapted);
        return Ok(paginationResponse);
    }


    [AllowAnonymous]
    [HttpGet(ApiRoutes.Apartment.GetAllByCity)]
    public async Task<IActionResult> GetAllByCity(string city, [FromQuery] PaginationQuery pagination)
    {
        var objs = await _apartmentService.GetAllByCityAsync(city, pagination.Adapt<PaginationFilter>());
        
        var adapted = objs.Adapt<List<ApartmentDTO>>();
        for (var index = 0; index < objs.Count; index++)
        {
            var item = objs[index];
            if (item.Image != null)
            {
                adapted[index].ImageBase64 = Convert.ToBase64String(item.Image);
            }
        }
        
        if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            return Ok(new PagedResponse<ApartmentDTO>(adapted));

        var paginationResponse = PaginationHelpers.CreatePaginatedResponse(_uriService, pagination, adapted);
        return Ok(paginationResponse);
    }
    
    
    [AllowAnonymous]
    [HttpGet(ApiRoutes.Apartment.GetAllByAddress)]
    public async Task<IActionResult> GetAllByAddress(string address)
    {
        var objs = await _apartmentService.GetAllByAddressAsync(address);

        var adapted = objs.Adapt<List<ApartmentDTO>>();
        for (var index = 0; index < objs.Count; index++)
        {
            var item = objs[index];
            if (item.Image != null)
            {
                adapted[index].ImageBase64 = Convert.ToBase64String(item.Image);
            }
        }

        return Ok(objs);
    }
    
    [Authorize]
    [HttpGet(ApiRoutes.Apartment.GetMine)]
    public async Task<IActionResult> GetMyApartmentAsync()
    {
        var obj = await _apartmentService.GetMineAsync();
        var adapted = obj.Adapt<ApartmentDTO>();
        if (obj.Image != null)
        {
            adapted.ImageBase64 = Convert.ToBase64String(obj.Image);
        }

        return Ok(adapted);
    }

    [Authorize]
    [HttpPost(ApiRoutes.Apartment.Create)]
    public async Task<IActionResult> CreateAsync([FromBody]CreateApartmentRequestModel request)
    {
        var obj = await _apartmentService
            .CreateMineAsync(request.Adapt<ApartmentServiceModel>());
        return Ok(obj);
    }

    [Authorize]
    [HttpPut(ApiRoutes.Apartment.Update)]
    public async Task<IActionResult> UpdateAsync([FromBody]UpdateApartmentRequestModel request)
    {
        await _apartmentService
            .UpdateMineAsync(request.Adapt<ApartmentServiceModel>());
        return Ok();
    }

    [Authorize]
    [HttpDelete(ApiRoutes.Apartment.Delete)]
    public async Task<IActionResult> DeleteAsync()
    {
        await _apartmentService.DeleteMineAsync();
        return Ok();
    }
}