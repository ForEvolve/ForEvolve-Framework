using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Microsoft.AspNetCore.Mvc.Testing
{
    public static class WebApplicationFactoryExtensions
    {
        public static IServiceCollection FindServiceCollection<TEntryPoint>(this WebApplicationFactory<TEntryPoint> webApplicationFactory)
             where TEntryPoint : class
        {
            // Make sure the server is started
            if (webApplicationFactory.Server?.Host == null)
            {
                throw new XunitException("The Server and the Host cannot be null. Please make sure the server is started. By default, this is accomplished by creating a client.");
            }

            // Try to find the _applicationServiceCollection private field on the Host
            var host = webApplicationFactory.Server.Host;
            var field = host.GetType().GetField(
                "_applicationServiceCollection",
                BindingFlags.Instance | BindingFlags.NonPublic
            );
            var value = field.GetValue(host);

            // Make sure the IServiceCollection exists
            if (!(value is IServiceCollection services))
            {
                throw new XunitException("The IServiceCollection was not found. Maybe the _applicationServiceCollection field was renamed or removed. If the problem persist, please open a GitHub issue in the project repository.");
            }
            return services;
        }
    }
}
