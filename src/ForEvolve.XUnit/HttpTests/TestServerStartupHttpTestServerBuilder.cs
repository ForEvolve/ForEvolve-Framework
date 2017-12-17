using Microsoft.Extensions.DependencyInjection;
using System;

namespace ForEvolve.XUnit.HttpTests
{
    public class TestServerStartupHttpTestServerBuilder : HttpTestServerBuilder<TestServerStartup>, ITestServerStartupHttpTestServerBuilder
    {
        private readonly Action<IServiceCollection> _configureServices;

        public TestServerStartupHttpTestServerBuilder(Action<IServiceCollection> configureServices = null)
        {
            _configureServices = configureServices;
        }

        public IHttpTestServer Arrange<TStatusCodeProvider>()
            where TStatusCodeProvider : class, IStatusCodeProvider
        {
            return Arrange(hostBuilder =>
            {
                hostBuilder.ConfigureServices(services =>
                {
                    services.AddTransient<IStatusCodeProvider, TStatusCodeProvider>();
                    _configureServices?.Invoke(services);
                });
            });
        }

        public IHttpTestServer Arrange<TStatusCodeProvider, TResponseProvider>()
            where TStatusCodeProvider : class, IStatusCodeProvider
            where TResponseProvider : class, IResponseProvider
        {
            return Arrange(hostBuilder =>
            {
                hostBuilder.ConfigureServices(services =>
                {
                    services.AddTransient<IStatusCodeProvider, TStatusCodeProvider>();
                    services.AddTransient<IResponseProvider, TResponseProvider>();
                    _configureServices?.Invoke(services);
                });
            });
        }

        public IHttpTestServer Arrange(IStatusCodeProvider statusCodeProvider)
        {
            return Arrange(hostBuilder =>
            {
                hostBuilder.ConfigureServices(services =>
                {
                    services.AddTransient(x => statusCodeProvider);
                    _configureServices?.Invoke(services);
                });
            });
        }

        public IHttpTestServer Arrange(IResponseProvider responseProvider)
        {
            return Arrange(hostBuilder =>
            {
                hostBuilder.ConfigureServices(services =>
                {
                    services.AddTransient(x => responseProvider);
                    _configureServices?.Invoke(services);
                });
            });
        }

        public IHttpTestServer Arrange(IStatusCodeProvider statusCodeProvider, IResponseProvider responseProvider)
        {
            return Arrange(hostBuilder =>
            {
                hostBuilder.ConfigureServices(services =>
                {
                    services.AddTransient(x => statusCodeProvider);
                    services.AddTransient(x => responseProvider);
                    _configureServices?.Invoke(services);
                });
            });
        }

        public IHttpTestServer Arrange<TStatusCodeProvider>(IResponseProvider responseProvider)
            where TStatusCodeProvider : class, IStatusCodeProvider
        {
            return Arrange(hostBuilder =>
            {
                hostBuilder.ConfigureServices(services =>
                {
                    services.AddTransient<IStatusCodeProvider, TStatusCodeProvider>();
                    services.AddTransient(x => responseProvider);
                    _configureServices?.Invoke(services);
                });
            });
        }

        public IHttpTestServer Arrange<TResponseProvider>(IStatusCodeProvider statusCodeProvider)
            where TResponseProvider : class, IResponseProvider
        {
            return Arrange(hostBuilder =>
            {
                hostBuilder.ConfigureServices(services =>
                {
                    services.AddTransient(x => statusCodeProvider);
                    services.AddTransient<IResponseProvider, TResponseProvider>();
                    _configureServices?.Invoke(services);
                });
            });
        }
    }
}
