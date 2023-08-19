using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Carrier, CarrierDto>().ReverseMap();
            CreateMap<CarrierConfiguration, CarrierConfigurationDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();

        }
    }
}
