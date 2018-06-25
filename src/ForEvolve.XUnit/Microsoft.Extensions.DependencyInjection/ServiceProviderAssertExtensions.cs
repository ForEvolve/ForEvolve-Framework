using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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
            Assert.IsType<TImplementation>(service);
            return serviceProvider;
        }

        public static IServiceProvider AssertServicesImplementationExists<TInterface, TImplementation>(this IServiceProvider serviceProvider)
        {
            var services = serviceProvider.GetServices<TInterface>();
            var exists = services.Any(x => x.GetType() == typeof(TImplementation));
            Assert.True(exists);
            return serviceProvider;
        }
    }
}
