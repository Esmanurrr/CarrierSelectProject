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
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.OrderDesi).IsRequired();
            builder.Property(x => x.OrderDate).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.OrderCarrierCost).IsRequired().HasColumnType("decimal (16,3)");

        }
    }
}
