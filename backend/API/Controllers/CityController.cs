using API.Contracts.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Models;
using Services.Models.ServiceModels;

namespace API.Controllers;

[ApiController]
public class CityController : Controller
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [AllowAnonymous]
    [HttpGet(ApiRoutes.City.GetAll)]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _cityService.GetAllCitiesAsync());
    }
}