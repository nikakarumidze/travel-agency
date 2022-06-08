using Services.Abstractions;
using Services.Implementations;

namespace API.Infrastructure.Installers;

public class ServicesInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserService, UserService>();
    }
}