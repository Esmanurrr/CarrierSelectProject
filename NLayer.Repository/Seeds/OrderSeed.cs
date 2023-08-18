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
    public class OrderSeed : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasData(new Order()
            {
                Id = 1,
                OrderDesi = 13,
                OrderCarrierCost = 44
            },
            new Order()
            {
                Id = 2,
                OrderDesi = 12,
                OrderCarrierCost = 30
            });
        }
    }
}
