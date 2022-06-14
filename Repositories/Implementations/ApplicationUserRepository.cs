using Domain.POCOs;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;

namespace Repositories.Implementations;

public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly IBaseRepository<ApplicationUser> _baseRepository;

    public ApplicationUserRepository(IBaseRepository<ApplicationUser> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public async Task<ApplicationUser> GetFullInfoAsync(string username)
    {
        var objs = await _baseRepository.TableAsNoTracking
            .Include(x => x.MyHosts)
            .Include(x => x.MyTravels)
            .Include(x=>x.Apartment)
            .SingleOrDefaultAsync(x => x.UserName == username);
        return objs;
    }
}