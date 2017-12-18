using Microsoft.Extensions.DependencyInjection;
using System;

namespace ForEvolve.XUnit.HttpTests
{
    public class TestServerStartupHttpTestServerBuilder : HttpTestServerBuilder<TestServerStartup>, ITestServerStartupHttpTestServerBuilder
    {
        private readonly Action<IServiceCollection> _configureServices;
        public const string ArrangeNotSupportedExceptionMessage = "You must register an IStatusCodeProvider for the TestServerStartup class to return an HTTP status code. Impossible to use this method, use another Arange method instead.";

        public TestServerStartupHttpTestServerBuilder(Action<IServiceCollection> configureServices = null)
        {
            _configureServices = configureServices;
        }

        public override IHttpTestServer Arrange()
        {
            throw new NotSupportedException(ArrangeNotSupportedExceptionMessage);
        }

        public virtual IHttpTestServer Arrange<TStatusCodeProvider>()
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

        public virtual IHttpTestServer Arrange<TStatusCodeProvider, TResponseProvider>()
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

        public virtual IHttpTestServer Arrange(IStatusCodeProvider statusCodeProvider)
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

        public virtual IHttpTestServer Arrange(IResponseProvider responseProvider)
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

        public virtual IHttpTestServer Arrange(IStatusCodeProvider statusCodeProvider, IResponseProvider responseProvider)
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

        public virtual IHttpTestServer Arrange<TStatusCodeProvider>(IResponseProvider responseProvider)
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

        public virtual IHttpTestServer Arrange<TResponseProvider>(IStatusCodeProvider statusCodeProvider)
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
