using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {


        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Order> CreateOrder(Order order)
        {
            await _context.AddAsync(order);
            return order;
        }
    }
}
