using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Repository.Configurations;

namespace NLayer.API.Controllers
{
  
    public class CarrierConfigurationController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly ICarrierConfigurationService _service;


        public CarrierConfigurationController(IMapper mapper, ICarrierConfigurationService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var carrierConfig = await _service.GetAllAsync();
            var carriersConfigDto = _mapper.Map<List<CarrierConfigurationDto>>(carrierConfig.ToList());
            return CreateActionResult(CustomResponseDto<List<CarrierConfigurationDto>>.Success(200, carriersConfigDto));
        }

        [HttpPost]
        public async Task<IActionResult> Add(CarrierConfigurationDto carrierConfigDto)
        {
            var carrierConfig = await _service.AddAsync(_mapper.Map<CarrierConfiguration>(carrierConfigDto));
            var carriersConfigDto = _mapper.Map<CarrierConfigurationDto>(carrierConfig);
            return CreateActionResult(CustomResponseDto<CarrierConfigurationDto>.Success(201));// 201 - created

        }

        [HttpPut]
        public async Task<IActionResult> Update(CarrierConfigurationDto carrierConfigDto)
        {
            await _service.UpdateAsync(_mapper.Map<CarrierConfiguration>(carrierConfigDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var carrierConfig = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(carrierConfig);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }
    }
}