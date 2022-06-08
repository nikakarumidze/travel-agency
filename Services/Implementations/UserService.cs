using Domain.POCOs;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using Services.Exceptions;
using Services.Localisations;
using Services.Models;

namespace Services.Implementations;

public class UserService : IUserService
{
    private readonly IJwtService _jwtService;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(IJwtService jwtService,UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _jwtService = jwtService;
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
            var entityByName = await _userManager.FindByNameAsync(user.Username);
            var entityByEmail = await _userManager.FindByEmailAsync(user.Email);
            if (entityByName != null || entityByEmail!=null)
                throw new ObjectAlreadyExistsException(ExceptionMessages.ObjectAlreadyExists);
            var applicationUser = new ApplicationUser()
            {
                UserName = user.Username,
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
}