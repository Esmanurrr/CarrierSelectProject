using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    public class CarrierSeed : IEntityTypeConfiguration<Carrier>
    {
        public void Configure(EntityTypeBuilder<Carrier> builder)
        {
            builder.HasData(new Carrier()
            {
                Id = 1,
                CarrierName = "MNG",
                CarrierIsActive = true,
                CarrierPlusDesiCost = 4
            },
            new Carrier()
            {
                Id = 2,
                CarrierName = "UPS",
                CarrierIsActive = true,
                CarrierPlusDesiCost = 3
            });
           
        }
    }
}
