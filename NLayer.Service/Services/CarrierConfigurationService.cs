using AutoMapper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository.Configurations;
using NLayer.Repository.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class CarrierConfigurationService : Service<CarrierConfiguration>, ICarrierConfigurationService
    {
        public CarrierConfigurationService(IGenericRepository<CarrierConfiguration> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
