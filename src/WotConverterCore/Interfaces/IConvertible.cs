using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WotConverterCore.Models;

namespace WotConverterCore.Interfaces
{
    public interface IConvertible<K> : IGenericConvertible
    {
        void ConvertFrom(K value) { }
    }
}
