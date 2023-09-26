using AutoMapper;
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
        /* CarrierId
         * CarrierName
         * CarrierIsActive
         * CarrierPlusDesiCost
         * CarrierConfigurationId
         */
        private readonly ICarrierConfigurationRepository _carrierConfigRepository;
        /* CarrierConfigurationId
         * CarrierId
         * CarrierMaxDesi
         * CarrierMinDesi
         * CarrierCost
         */

        private readonly IMapper _mapper;

        public OrderService(IGenericRepository<Order> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public async Task<CustomResponseDto<Order>> CreateOrderAsync(Order order)
        {

            List<CarrierConfiguration> carrierConfigurations = _carrierConfigRepository.GetAll().ToList();

            List<Carrier> carriers = _carrierRepository.GetAll().ToList();

            var suitableCarrier = FindSuitableCarrier(carriers, carrierConfigurations, order.OrderDesi);

            var carrierCost = CalculateCarrierCost(suitableCarrier, order.OrderDesi);

            order.OrderCarrierCost = carrierCost;
            order.CarrierId = suitableCarrier.Id;
            await _orderRepository.AddAsync(order);

            var orderDto = _mapper.Map<OrderDto>(order);
            return CustomResponseDto<Order>.Success(204, order); //its false look again.


            Carrier FindSuitableCarrier(IEnumerable<Carrier> carriers, IEnumerable<CarrierConfiguration> carrierConfigurations, decimal desi)
            {
                // Herhangi bir kargo firmasının MinDesi-MaxDesi aralığına giriyor ise
                var availableCarriers = carrierConfigurations.Where(x => x.CarrierMinDesi <= desi).Where(x => x.CarrierMaxDesi >= desi).ToList();

                // Bulunan kargo firmaları arasından en düşük ücrete sahip 1 adet kargo firması seç
                if (availableCarriers.Any())
                {
                    return availableCarriers.OrderBy(x => x.CarrierCost).First().Carrier;
                }

                // Herhangi bir kargo firmasının MinDesi-MaxDesi aralığına girmiyor ise
                else
                {
                    // CarrierConfigurations tablosunda bulunan veriler içerisinden, sipariş desi değerine en yakın olan kargo firmasının verileri gerekli sorgular ile getir
                    var closestCarrier = carrierConfigurations.OrderBy(c => Math.Abs(desi - carrierConfigurations.First(cc => cc.CarrierId == c.Id).CarrierMaxDesi)).First().Carrier;

                    // En yakın kargo firmasını döndür
                    return closestCarrier;
                }
            }

            decimal CalculateCarrierCost(Carrier carrier, decimal desi)
            {
                // Siparişin desi miktarı kargo firmasının MinDesi-MaxDesi aralığına giriyor ise
                var carrierConfiguration = _carrierConfigRepository.GetAll().First(cc => cc.CarrierId == carrier.Id);

                if (carrierConfiguration.CarrierMinDesi <= desi && carrierConfiguration.CarrierMaxDesi >= desi)
                {
                    // Kargo firmasının fiyat değerini döndür
                    return carrierConfiguration.CarrierCost;
                }

                // Siparişin desi miktarı kargo firmasının MinDesi-MaxDesi aralığına girmiyor ise
                else
                {
                    // Siparişe ait desi miktarı ile kargo firmasına ait en yakın desi miktarı arasındaki fark bulunur
                    var difference = Math.Abs(desi - carrierConfiguration.CarrierMaxDesi);

                    // Fark değeri ile +1 desi fiyatı çarpılarak kargo fiyatı ile toplanır
                    var finalCarrierCost = carrierConfiguration.CarrierCost + (difference * carrier.CarrierPlusDesiCost);

                    // Son kargo ücretini döndür
                    return finalCarrierCost;
                }
            }

        }
    }
}
