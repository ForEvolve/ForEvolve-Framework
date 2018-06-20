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
        public static IServiceProvider AssertService<TInterface>(this IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetRequiredService<TInterface>();
            return serviceProvider;
        }

        public static IServiceProvider AssertService<TInterface, TImplementation>(this IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetRequiredService<TInterface>();
            Assert.IsType<TImplementation>(service);
            return serviceProvider;
        }

        public static IServiceProvider AssertServices<TInterface, TImplementation>(this IServiceProvider serviceProvider)
        {
            var services = serviceProvider.GetServices<TInterface>();
            services.First(x => x.GetType() == typeof(TImplementation));
            return serviceProvider;
        }
    }
}
