using Domain.POCOs;

namespace Repositories.Abstractions;

public interface IApplicationUserRepository
{
    Task<ApplicationUser> GetFullInfoAsync(string username);
}