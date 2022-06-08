using System.Net;
using DBContext.Context;
using Domain.POCOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DBContext.Seeds;

public static class TravelDbSeeds
{
    public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        using var scope = serviceProvider.CreateScope();
        var database = scope.ServiceProvider.GetRequiredService<TravelDbContext>();
        Migrate(database);
        await SeedEverything(database, userManager, roleManager);
    }

    private static void Migrate(TravelDbContext context)
    {
        context.Database.Migrate();
    }

    private static async Task SeedEverything(DbContext context, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        await SeedRoles(roleManager);
        await SeedApplicationUsers(userManager);
        await context.SaveChangesAsync();
    }

    private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        if (roleManager.Roles.Count() != 3)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("Moderator"));
            await roleManager.CreateAsync(new IdentityRole("Basic"));
        }
    }

    private static async Task SeedApplicationUsers(UserManager<ApplicationUser> userManager)
    {
        var defaultUser = new ApplicationUser
        {
            UserName = "admin",
            Email = "natenadzedat@gmail.com",
            Firstname = "Dato",
            Lastname = "Natenadze",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            Apartment = new Apartment
            {
                Address = "Viktor Dolidze 25",
                BedsNumber = 2,
                City = "Tbilisi",
                Conditioner = true,
                DistanceToCenter = "2",
                Gym = false,
                Image = null,
                Parking = true, 
                Wifi = true, 
                Pool = false
            }
        };
        var user = await userManager.FindByEmailAsync(defaultUser.Email);
        if (user == null)
        {
            await userManager.CreateAsync(defaultUser, "Dato123123!");

            await userManager.AddToRoleAsync(defaultUser, "Admin");
            await userManager.AddToRoleAsync(defaultUser, "Moderator");
            await userManager.AddToRoleAsync(defaultUser, "Basic");
        }
    }
}