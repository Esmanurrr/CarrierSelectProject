using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarriersController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Carrier> _service;

        public CarriersController(IMapper mapper, IService<Carrier> service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var carrier = await _service.GetAllAsync();
            var carriersDto = _mapper.Map<List<CarrierDto>>(carrier.ToList());
            return CreateActionResult(CustomResponseDto<List<CarrierDto>>.Success(200, carriersDto));
        }

        [HttpPost]
        public async Task<IActionResult> Add(CarrierDto carrierDto)
        {
            var carrier = await _service.AddAsync(_mapper.Map<Carrier>(carrierDto));
            var carriersDto = _mapper.Map<CarrierDto>(carrier);
            return CreateActionResult(CustomResponseDto<CarrierDto>.Success(201));// 201 - created

        }

        [HttpPut]
        public async Task<IActionResult> Update(CarrierDto carrierDto)
        {
            await _service.UpdateAsync(_mapper.Map<Carrier>(carrierDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var carrier = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(carrier);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }


    }
}
