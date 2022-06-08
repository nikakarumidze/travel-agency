using Domain.POCOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DBContext.Context;

public class TravelDbContext : IdentityDbContext<ApplicationUser>
{
    public TravelDbContext(DbContextOptions<TravelDbContext> options) : base(options){}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }    
}