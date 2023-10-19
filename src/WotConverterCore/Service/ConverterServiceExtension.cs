using Microsoft.Extensions.DependencyInjection;
using WotConverterCore.Interfaces;

namespace WotConverterCore.Service
{
    public static class ConverterServiceExtension
    {
        public static void AddThingModelConverter<K>(this ServiceCollection serviceCollection) where K : class, ITmConvertible<K>, new()
        {
            serviceCollection.AddScoped<ITmConvertible<K>, K>();
        }

    }
}
