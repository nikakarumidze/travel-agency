using API.Contracts.V1;
using API.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Models;

namespace API.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPut(ApiRoutes.User.SignIn)]
    public async Task<IActionResult> SignInAsync(LogInRequestModel request)
    {
        var token = await _userService.AuthenticationAsync(request.Username, request.Password);
        return Ok(new {token.Item1, token.Item2});
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.User.Register)]
    public async Task<IActionResult> RegistrationAsync(CreateUserRequestModel request)
    {
        await _userService.CreateAsync(request.Adapt<CreateUserServiceModel>());
        return Ok();
    }
    //ToDo
    [Authorize]
    [HttpPut(ApiRoutes.User.ChangePassword)]
    public async Task<IActionResult> ChangePasswordAsync()
    {
        var username = HttpContext.User.Identity.Name;
        return Ok("Signed in");
    }
}