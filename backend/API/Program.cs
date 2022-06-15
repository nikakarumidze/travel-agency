using System.Diagnostics;
using System.Text.Json.Serialization;
using API.Infrastructure.Extensions;
using API.Infrastructure.Middlewares;
using DBContext.Seeds;
using Domain.POCOs;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InstallServicesFromAssembly(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();