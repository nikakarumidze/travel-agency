using API.Contracts.Responses;
using API.Contracts.V1;
using API.Models.DTOs;
using API.Models.UserRequests.UserRequestModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Models.UserRequestServiceModels;

namespace API.Controllers;

[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [Authorize]
    [HttpGet(ApiRoutes.User.GetInfo)]
    public async Task<IActionResult> GetInfoAsync()
    {
        var user = await _userService.GetInfoAsync();
        return Ok(user.Adapt<ApplicationUserDTO>());
    }
    
    [AllowAnonymous]
    [HttpPut(ApiRoutes.User.SignIn)]
    public async Task<IActionResult> SignInAsync([FromBody] LogInRequestModel request)
    {
        var token = await _userService.AuthenticationAsync(request.Username, request.Password);
        return Ok(new AuthSuccessResponse
        {
            Token = token.Item1,
            RefreshToken = token.Item2
        });
    }
    
    [AllowAnonymous]
    [HttpPost(ApiRoutes.User.Register)]
    public async Task<IActionResult> RegistrationAsync(CreateUserRequestModel request)
    {
        var errors = await _userService.CreateAsync(request.Adapt<CreateUserServiceModel>());
        return Ok();
    }
    [Authorize]
    [HttpPut(ApiRoutes.User.ChangePassword)]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequestModel request)
    {
        var result = await _userService.ChangePasswordAsync(request.Username, request.OldPassword, request.NewPassword);
        return Ok(result ? "Password Successfully changed" : "Password could not be changed");
    }

    [Authorize]
    [HttpPut(ApiRoutes.User.UpdateUserInfo)]
    public async Task<IActionResult> UpdateAsync([FromBody]UpdateUserInfoRequestModel requestModel)
    {
        await _userService
            .UpdateUserInfoAsync(requestModel.Adapt<UpdateUserInfoServiceModel>());
        return Ok();
    }
    
    [HttpPut(ApiRoutes.User.Refresh)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var token = await _userService.RefreshTokenAsync(request.Token, request.RefreshToken);
        return Ok(new AuthSuccessResponse
        {
            Token = token.Item1,
            RefreshToken = token.Item2
        });
    }
}