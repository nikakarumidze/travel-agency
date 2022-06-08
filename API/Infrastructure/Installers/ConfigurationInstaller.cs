using Services.Configurations;

namespace API.Infrastructure.Installers;

public class ConfigurationInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfiguration>(configuration.GetSection(nameof(JwtConfiguration)));
        //services.Configure<EmailConfiguration>(configuration.GetSection(nameof(EmailConfiguration)));
    }
}