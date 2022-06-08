using System.Net.Mime;
using DBContext.Configurations;
using Domain.POCOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DBContext.Context;

public class TravelDbContext : IdentityDbContext<ApplicationUser>
{
    public TravelDbContext(DbContextOptions<TravelDbContext> options) : base(options){}
    
    public DbSet<Apartment> Apartments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TravelDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }    
}