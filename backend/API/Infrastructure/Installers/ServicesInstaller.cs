using Services.Abstractions;
using Services.Implementations;

namespace API.Infrastructure.Installers;

public class ServicesInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IApartmentService, ApartmentService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderProcessor, OrderProcessor>();
        
        services.AddSingleton<IUriService>(provider =>
        {
            var accessor = provider.GetRequiredService<IHttpContextAccessor>();
            var request = accessor.HttpContext.Request;
            var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), "/");
            return new UriService(absoluteUri);
        });
    }
}