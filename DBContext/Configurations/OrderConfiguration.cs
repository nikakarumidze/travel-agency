using Domain.POCOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DBContext.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Guest)
            .WithMany(x => x.MyTravels)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Host)
            .WithMany(x => x.MyHosts)
            .OnDelete(DeleteBehavior.NoAction);
    }
}