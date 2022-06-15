using Domain.POCOs;
using Microsoft.AspNetCore.Identity;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Exceptions;
using Services.Localisations;
using Services.Models.UserRequestServiceModels;

namespace Services.Implementations;

public class UserService : IUserService
{
    private readonly IJwtService _jwtService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationUserRepository _applicationUserRepository;

    public UserService(IJwtService jwtService, UserManager<ApplicationUser> userManager, 
    IApplicationUserRepository applicationUserRepository)
    {
        _userManager = userManager;
        _applicationUserRepository = applicationUserRepository;
        _jwtService = jwtService;
    }

    public async Task<ApplicationUser> GetInfoAsync(string username)
    {
        var user = await _applicationUserRepository.GetFullInfoAsync(username);
        return user;
    }

    public async Task<(string, DateTime?)> AuthenticationAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
            throw new InvalidCredentialsException(ExceptionMessages.InvalidCredentials);
        var check = await _userManager.CheckPasswordAsync(user, password);
        if (!check)
            throw new InvalidCredentialsException(ExceptionMessages.InvalidCredentials);
        return _jwtService.GenerateSecurityToken(username);
    }

    public async Task<IEnumerable<IdentityError>> CreateAsync(CreateUserServiceModel user)
    {
        var entityByName = await _userManager.FindByNameAsync(user.UserName);
        var entityByEmail = await _userManager.FindByEmailAsync(user.Email);
        if (entityByName != null || entityByEmail !=null)
            throw new ObjectAlreadyExistsException(ExceptionMessages.ObjectAlreadyExists);
        var applicationUser = new ApplicationUser()
        {
            UserName = user.UserName,
            Email = user.Email,
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await _userManager.CreateAsync(applicationUser, user.Password);
        if (result != null)
            return result.Errors;
        await _userManager.AddToRoleAsync(applicationUser, "Basic");
        return null;
    }

    public async Task<bool> ChangePasswordAsync(string username, string oldPassword, string newPassword)
    {
        var user = await _userManager.FindByNameAsync(username);
        var check = await _userManager.CheckPasswordAsync(user, oldPassword);
        if (check is false)
            throw new InvalidCredentialsException(ExceptionMessages.InvalidCredentials);
        await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        return true;
    }

    public async Task UpdateUserInfoAsync(UpdateUserInfoServiceModel request)
    {
        var user = await _userManager.FindByIdAsync(request.Id);

        user.UserName = request.UserName;
        user.Firstname = request.Firstname;
        user.Lastname = request.Lastname;
        user.Image = request.Image;
        
        await _userManager.UpdateAsync(user);
    }
}