using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceProviderAssertExtensions
    {
        public static IServiceProvider AssertServiceExists<TInterface>(this IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetRequiredService<TInterface>();
            return serviceProvider;
        }

        public static IServiceProvider AssertServiceImplementationExists<TInterface, TImplementation>(this IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetRequiredService<TInterface>();
            try
            {
                Assert.IsType<TImplementation>(service);
            }
            catch (IsTypeException)
            {
                throw new IsTypeException(typeof(TImplementation).Name, service.GetType().Name);
            }
            return serviceProvider;
        }

        public static IServiceProvider AssertServicesImplementationExists<TInterface, TImplementation>(this IServiceProvider serviceProvider)
        {
            var services = serviceProvider.GetServices<TInterface>();
            var exists = services.Any(x => x.GetType() == typeof(TImplementation));
            if (!exists)
            {
                throw new TrueException($"No implementation of type {typeof(TImplementation)} was found for service type {typeof(TInterface)}.", exists);
            }
            return serviceProvider;
        }
    }
}
