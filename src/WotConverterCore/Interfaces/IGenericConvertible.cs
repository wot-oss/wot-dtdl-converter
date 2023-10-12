using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace WotConverterCore.Interfaces
{
    public interface IGenericConvertible
    {
       static bool CanConvert<K>(K type) => true;
    }
}
