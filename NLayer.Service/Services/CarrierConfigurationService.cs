using AutoMapper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class CarrierConfigurationService : Service<CarrierConfiguration>,  ICarrierConfigurationService
    {

        private readonly ICarrierConfigurationRepository _carrierConfigRepository;
        private readonly ICarrierRepository _carrierRepository;
        private readonly IMapper _mapper;

        public CarrierConfigurationService(IGenericRepository<CarrierConfiguration> repository, IUnitOfWork unitOfWork, ICarrierConfigurationRepository carrierConfigRepository, IMapper mapper, ICarrierRepository carrierRepository) : base(repository, unitOfWork)
        {
            _carrierConfigRepository = carrierConfigRepository;
            _mapper = mapper;
            _carrierRepository = carrierRepository;
        }

    

        public async Task<List<CarrierConfiguration>> GetAvailableCarrier(int desi)
        {
            var carrierConfig =await _carrierConfigRepository.GetCarrierConfigWithCarrier();
 
            var available = carrierConfig.Where(x => x.CarrierMinDesi <= desi && x.CarrierMaxDesi >= desi).ToList();
            
            return available;

            var active = _carrierRepository.GetWhere(x => x.CarrierIsActive == true);

            /*
            if (active.Any())
            {
                if (available.Any())//if the desi in correct place
                {
                    var carrier = available.OrderBy(x => x.CarrierCost).FirstOrDefault();
                    var cost = carrier.CarrierCost;
      stcarrierConfig.Id).First();
              return carrier;

                }
                else
                {
                    var closestcarrierConfig = carrierConfig.OrderBy(c => Math.Abs(desi - c.CarrierMaxDesi)).First();

                    var closestCarrier = _carrierRepository.GetWhere(x => x.Id == close
                    var difference = desi - closestcarrierConfig.CarrierMaxDesi;

                    var finalCost = closestcarrierConfig.CarrierCost + (difference * closestCarrier.CarrierPlusDesiCost);

                    return finalCost;

                }
            }

            return 0;
            */

            return available;
        }


        

    }
}
