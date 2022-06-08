using API.Infrastructure.Installers;

namespace API.Infrastructure.Extensions;

public static class ServiceExtension
{
    public static void InstallServicesFromAssembly(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddHttpContextAccessor();
        var installers = typeof(Program).Assembly.ExportedTypes
            .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
        installers.ForEach(installer=>installer.InstallServices(services, configuration));
        services.AddTokenAuthentication(configuration);
    }
}