using Domain.POCOs;
using Microsoft.AspNetCore.Identity;
using Services.Models.UserRequestServiceModels;

namespace Services.Abstractions;

public interface IUserService
{
    Task<ApplicationUser> GetInfoAsync(string username);
    Task<(string, DateTime?)> AuthenticationAsync(string username, string password);
    Task<IEnumerable<IdentityError>> CreateAsync(CreateUserServiceModel user);
    Task<bool> ChangePasswordAsync(string username, string oldPassword, string newPassword);
    Task UpdateUserInfoAsync(UpdateUserInfoServiceModel request);
}   