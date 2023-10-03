using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class OrdersController : CustomBaseController
    {

        private readonly IMapper _mapper;
        private readonly IOrderService _service;

        public OrdersController(IMapper mapper, IOrderService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _service.GetAllAsync();
            var ordersDto = _mapper.Map<List<OrderDto>>(orders.ToList());
            return CreateActionResult(CustomResponseDto<List<OrderDto>>.Success(200, ordersDto));

        }

        [HttpPost("{orderDesi}")]
        public async Task<IActionResult> Create(int orderDesi)
        {
            await _service.CreateOrderAsync(orderDesi);
            
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
