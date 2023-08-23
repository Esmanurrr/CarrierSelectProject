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
    public class CarrierRepository : GenericRepository<Carrier>, ICarrierRepository
    {

        public CarrierRepository(AppDbContext context) : base(context)
        {
            
        }

        
    }
}
