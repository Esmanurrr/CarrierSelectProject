using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    public class CarrierConfigurationSeed : IEntityTypeConfiguration<CarrierConfiguration>
    {
        public void Configure(EntityTypeBuilder<CarrierConfiguration> builder)
        {
            builder.HasData(new CarrierConfiguration()
            {
                Id = 1,
                CarrierMaxDesi = 10,
                CarrierMinDesi = 2,
                CarrierCost = 22
            },
            new CarrierConfiguration()
            {
                Id = 2,
                CarrierMaxDesi = 15,
                CarrierMinDesi = 3,
                CarrierCost = 20
            });
        }
    }
}
