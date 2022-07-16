using Domain;
using Domain.POCOs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DBContext.Context;

public class TravelDbContext : IdentityDbContext<ApplicationUser>
{
    public TravelDbContext(DbContextOptions<TravelDbContext> options) : base(options){}
    
    public DbSet<Apartment> Apartments { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<ApplicationUser> AspNetUsers { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TravelDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }    
}