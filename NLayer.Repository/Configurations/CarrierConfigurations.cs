using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Configurations
{
    public class CarrierConfigurations : IEntityTypeConfiguration<Carrier>
    {
        public void Configure(EntityTypeBuilder<Carrier> builder)
        {
            builder.Property(x => x.CarrierName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CarrierIsActive).IsRequired();
            builder.Property(x => x.CarrierPlusDesiCost).IsRequired();
            

        }
    }
}
