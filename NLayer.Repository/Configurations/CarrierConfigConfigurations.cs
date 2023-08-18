using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Configurations
{
    public class CarrierConfigConfigurations : IEntityTypeConfiguration<CarrierConfiguration>
    {
        public void Configure(EntityTypeBuilder<CarrierConfiguration> builder)
        {
            builder.Property(x => x.CarrierMaxDesi).IsRequired();
            builder.Property(x => x.CarrierMinDesi).IsRequired();
            builder.Property(x => x.CarrierCost).IsRequired();
        }
    }
}
