using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class Carrier : BaseEntity
    {
        public string CarrierName { get; set; }
        public bool CarrierIsActive { get; set; }
        public int CarrierPlusDesiCost { get; set; }
        public ICollection<Order> Orders { get; set; }//navigaetion property
        public ICollection<CarrierConfiguration> CarrierConfigurations { get; set; }//navigation property

    }

}
