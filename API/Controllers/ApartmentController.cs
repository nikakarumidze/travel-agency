using API.Contracts.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace API.Controllers;

public class ApartmentController : Controller
{
    private readonly IApartmentService _apartmentService;

    public ApartmentController(IApartmentService apartmentService)
    {
        _apartmentService = apartmentService;
    }

    [Authorize]
    [HttpGet(ApiRoutes.Apartment.GetMine)]
    public async Task<IActionResult> GetMyApartment(string id)
    {
        var obj = await _apartmentService.GetMyApartmentAsync(id);
        return Ok(obj);
    }
}