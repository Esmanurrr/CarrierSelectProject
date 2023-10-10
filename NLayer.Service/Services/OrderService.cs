using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class OrderService : Service<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICarrierRepository _carrierRepository;
        private readonly ICarrierConfigurationRepository _carrierConfigurationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICarrierConfigurationService _carrierConfigService;

        public OrderService(IGenericRepository<Order> repository, IUnitOfWork unitOfWork, IOrderRepository orderRepository, ICarrierRepository carrierRepository, ICarrierConfigurationRepository carrierConfigurationRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _orderRepository = orderRepository;
            _carrierRepository = carrierRepository;
            _carrierConfigurationRepository = carrierConfigurationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto<NoContentDto>> CreateOrderAsync(int orderDesi)
        {
            var desi = orderDesi;

            List<CarrierConfiguration> carrierConfigurations = await _carrierConfigurationRepository.GetAll().ToListAsync();
            List<Carrier> carriers = await _carrierRepository.GetAll().ToListAsync();

            bool foundMatchingConfiguration = false;
            decimal minCarrierCost = decimal.MaxValue;
            int selectedCarrierId = 0;

            foreach (Carrier carrier in carriers)
            {
                if (carrier.CarrierIsActive)
                {
                    // Find the carrier which is available for order desi range
                    foreach (CarrierConfiguration configuration in carrierConfigurations)
                    {
                        if (configuration.CarrierId == carrier.Id && desi >= configuration.CarrierMinDesi && desi <= configuration.CarrierMaxDesi)
                        {
                            foundMatchingConfiguration = true;

                            //Select the carrier with the lowest cost
                            if (configuration.CarrierCost < minCarrierCost)
                            {
                                minCarrierCost = configuration.CarrierCost;
                                selectedCarrierId = configuration.CarrierId;
                            }
                        }
                    }
                }
            }

            if (foundMatchingConfiguration)
            {
                await _orderRepository.AddAsync(new()
                {
                    OrderDesi = orderDesi,
                    OrderDate = DateTime.Now,
                    OrderCarrierCost = minCarrierCost,
                    CarrierId = selectedCarrierId
                });

                await _unitOfWork.CommitAsync();

                return CustomResponseDto<NoContentDto>.Success(204); 
            }

            decimal carrierCost = 0;
            int plusDesiCost = 0;
            int carrierId = 0;

            decimal enYakinDesiFarki = decimal.MaxValue;

            // the situation of no available carrier for desi range
            foreach (CarrierConfiguration configuration in carrierConfigurations)
            {
                // closest desi value for carriers desi range
                decimal desiFarki = Math.Abs(desi - configuration.CarrierMaxDesi);

                if (desiFarki < enYakinDesiFarki)
                {
                    enYakinDesiFarki = desiFarki;

                    foreach (Carrier carrier in carriers)
                    {
                        if (configuration.CarrierId == carrier.Id && carrier.CarrierIsActive)
                        {
                            plusDesiCost = carrier.CarrierPlusDesiCost;
                            carrierId = carrier.Id;
                        }
                    }

                    // Calculate carrierCost
                    carrierCost = configuration.CarrierCost + (plusDesiCost * enYakinDesiFarki);
                }
            }

            await _orderRepository.AddAsync(new()
            {
                OrderDesi = orderDesi,
                OrderDate = DateTime.Now,
                OrderCarrierCost = carrierCost,
                CarrierId = carrierId
            });

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(204);
        }
    }
}
