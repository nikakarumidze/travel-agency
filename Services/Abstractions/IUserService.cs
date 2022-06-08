using Microsoft.AspNetCore.Identity;
using Services.Models;

namespace Services.Abstractions;

public interface IUserService
{
    Task<(string, DateTime?)> AuthenticationAsync(string username, string password);
    Task<IEnumerable<IdentityError>> CreateAsync(CreateUserServiceModel user);
    //Task<List<UsersWithRolesServiceModel>> GetAllWithRolesAsync();
    //Task<List<ManageUserRolesServiceModel>> ManageAsync(string userId);
}   