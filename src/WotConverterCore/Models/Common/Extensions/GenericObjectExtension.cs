using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WotConverterCore.Models.Common;

namespace WotConverterCore.Extensions
{
    public static class GenericObjectExtension
    {
        public static bool IsEmpty(this object? self)
        {
            if (self == null)
                return false;

            var properties = self
                .GetType()
                .GetProperties()
                .Where(p => p.GetAccessors(nonPublic: false)
                .Any(_ => !_.IsStatic));

            return properties.All(_ => _.GetValue(self) == null);
        }

    }
}
