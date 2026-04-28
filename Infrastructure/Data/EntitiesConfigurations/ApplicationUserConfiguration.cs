using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntitiesConfigurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();

        builder.OwnsOne(x => x.Address, address =>
        {
            address.Property(a => a.City).IsRequired().HasMaxLength(100);
            address.Property(a => a.St).IsRequired().HasMaxLength(200);
            address.Property(a => a.ZipCode).IsRequired().HasMaxLength(50);
        });

    }
}
