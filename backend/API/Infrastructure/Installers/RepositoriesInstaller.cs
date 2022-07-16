using DBContext.Context;
using Repositories.Abstractions;
using Repositories.Implementations;

namespace API.Infrastructure.Installers;

public class RepositoriesInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IApartmentRepository, ApartmentRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
    }
}