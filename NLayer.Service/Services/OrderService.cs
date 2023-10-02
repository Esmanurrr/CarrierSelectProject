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
        private readonly ICarrierConfigurationService _carrierConfigService;
        /* CarrierConfigurationId
         * CarrierId
         * CarrierMaxDesi
         * CarrierMinDesi
         * CarrierCost
         */

        private readonly IMapper _mapper;

        public OrderService(IGenericRepository<Order> repository, IUnitOfWork unitOfWork,
            ICarrierConfigurationService carrierConfigService, ICarrierRepository carrierRepository) : base(repository, unitOfWork)
        {
            _carrierRepository = carrierRepository;
            _carrierConfigService = carrierConfigService; 
        }

        public async Task<CustomResponseDto<OrderDto>> CreateOrderAsync(Order order)
        {

            //List<CarrierConfiguration> carrierConfigurations = _carrierConfigRepository.GetAllAsync().

            //List<Carrier> carriers = _carrierRepository.GetAll().ToList();

            var suitableCarrier = _carrierConfigService.GetAvailableCarrier(order.OrderDesi);
            
            /*
            order.OrderCarrierCost = suitableCarrier;
            order.CarrierId = _carrierConfigService.GetWhere(x => x.CarrierCost == suitableCarrier).First().Id;
            await _orderRepository.AddAsync(order);
            */

            var orderDto = _mapper.Map<OrderDto>(order);
            return CustomResponseDto<OrderDto>.Success(204, orderDto); //its false look again.
         

        }
    }
}
