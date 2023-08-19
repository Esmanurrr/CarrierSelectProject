using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }
}
