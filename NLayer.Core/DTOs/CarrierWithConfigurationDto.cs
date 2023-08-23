using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CarrierWithConfigurationDto : CarrierDto
    {
        public CarrierConfigurationDto CarrierConfiguration { get; set; }
    }
}
