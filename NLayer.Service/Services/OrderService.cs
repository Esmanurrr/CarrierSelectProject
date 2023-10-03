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

        /* CarrierId
         * CarrierName
         * CarrierIsActive
         * CarrierPlusDesiCost
         * CarrierConfigurationId
         */
        private readonly ICarrierConfigurationService _carrierConfigService;

        public OrderService(IGenericRepository<Order> repository, IUnitOfWork unitOfWork, IOrderRepository orderRepository, ICarrierRepository carrierRepository, ICarrierConfigurationRepository carrierConfigurationRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _orderRepository = orderRepository;
            _carrierRepository = carrierRepository;
            _carrierConfigurationRepository = carrierConfigurationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /* CarrierConfigurationId
        * CarrierId
        * CarrierMaxDesi
        * CarrierMinDesi
        * CarrierCost
        */




        public async Task<CustomResponseDto<NoContentDto>> CreateOrderAsync(int orderDesi)
        {
            var siparisDesi = orderDesi;

            List<CarrierConfiguration> carrierConfigurations = await _carrierConfigurationRepository.GetAll().ToListAsync();
            List<Carrier> carriers = await _carrierRepository.GetAll().ToListAsync();

            bool foundMatchingConfiguration = false;
            decimal minKargoUcreti = decimal.MaxValue;
            int selectedCarrierId = 0;

            foreach (Carrier carrier in carriers)
            {
                if (carrier.CarrierIsActive)
                {
                    //Siparişin desi değerinin hangi kargo firmasının desi aralığına girdiğini bulma
                    foreach (CarrierConfiguration configuration in carrierConfigurations)
                    {
                        if (configuration.CarrierId == carrier.Id && siparisDesi >= configuration.CarrierMinDesi && siparisDesi <= configuration.CarrierMaxDesi)
                        {
                            foundMatchingConfiguration = true;

                            //Select the carrier with the lowest cost
                            if (configuration.CarrierCost < minKargoUcreti)
                            {
                                minKargoUcreti = configuration.CarrierCost;
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
                    OrderCarrierCost = minKargoUcreti,
                    CarrierId = selectedCarrierId
                });

                await _unitOfWork.CommitAsync();

                return CustomResponseDto<NoContentDto>.Success(204); 
            }

            decimal kargoUcreti = 0;
            int plusDesiCost = 0;
            int carrierId = 0;

            decimal enYakinDesiFarki = decimal.MaxValue; // En yakın desi farkını takip etmek için bir başlangıç değeri atama

            // Siparişin desi değerinin hiçbir kargo firmasının desi aralığına girmemesi durumu
            foreach (CarrierConfiguration configuration in carrierConfigurations)
            {
                // Siparişin desi değeri ile kargo yapılandırmasının en yakın desi değeri arasındaki farkı hesaplama
                decimal desiFarki = Math.Abs(siparisDesi - configuration.CarrierMaxDesi);

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

                    // Kargo ücretini hesaplama
                    kargoUcreti = configuration.CarrierCost + (plusDesiCost * enYakinDesiFarki);
                }
            }

            await _orderRepository.AddAsync(new()
            {
                OrderDesi = orderDesi,
                OrderDate = DateTime.Now,
                OrderCarrierCost = kargoUcreti,
                CarrierId = carrierId
            });

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(204);
        }
    }
}
