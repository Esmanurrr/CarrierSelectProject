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
    public class CarrierConfigurationRepository : GenericRepository<CarrierConfiguration>, ICarrierConfigurationRepository
    {
        public CarrierConfigurationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<CarrierConfiguration>> GetCarrierConfigWithCarrier()
        {
            return await _context.CarrierConfigurations.Include(x => x.Carrier).ToListAsync();
        }

    }
}
