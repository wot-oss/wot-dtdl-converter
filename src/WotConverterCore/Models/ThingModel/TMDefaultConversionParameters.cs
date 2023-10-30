using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WotConverterCore.Interfaces;

namespace WotConverterCore.Models.ThingModel
{
    public class TMDefaultConversionParameters : IConversionParameters
    {
        public bool InsertHrefs { get; set; } = false;
    }
}
