using Domain.POCOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DBContext.Configurations;

public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.City)
            .WithMany();
    }
}
