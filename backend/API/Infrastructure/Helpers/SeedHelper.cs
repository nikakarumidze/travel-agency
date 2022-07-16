using System.Diagnostics;
using DBContext.Seeds;
using Domain.POCOs;
using Microsoft.AspNetCore.Identity;

namespace API.Infrastructure.Helpers;

public static class SeedHelper
{
    public static void Seed(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        try
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            TravelDbSeeds.Initialize(serviceProvider,userManager, roleManager).Wait();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}