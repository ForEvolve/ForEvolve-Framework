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

        public override IHttpTestServer Create()
        {
            throw new NotSupportedException(ArrangeNotSupportedExceptionMessage);
        }

        public virtual IHttpTestServer Create<TStatusCodeProvider>()
            where TStatusCodeProvider : class, IStatusCodeProvider
        {
            return Create(hostBuilder =>
            {
                hostBuilder.ConfigureServices(services =>
                {
                    services.AddTransient<IStatusCodeProvider, TStatusCodeProvider>();
                    _configureServices?.Invoke(services);
                });
            });
        }

        public virtual IHttpTestServer Create<TStatusCodeProvider, TResponseProvider>()
            where TStatusCodeProvider : class, IStatusCodeProvider
            where TResponseProvider : class, IResponseProvider
        {
            return Create(hostBuilder =>
            {
                hostBuilder.ConfigureServices(services =>
                {
                    services.AddTransient<IStatusCodeProvider, TStatusCodeProvider>();
                    services.AddTransient<IResponseProvider, TResponseProvider>();
                    _configureServices?.Invoke(services);
                });
            });
        }

        public virtual IHttpTestServer Create(IStatusCodeProvider statusCodeProvider)
        {
            return Create(hostBuilder =>
            {
                hostBuilder.ConfigureServices(services =>
                {
                    services.AddTransient(x => statusCodeProvider);
                    _configureServices?.Invoke(services);
                });
            });
        }

        public virtual IHttpTestServer Create(IResponseProvider responseProvider)
        {
            return Create(hostBuilder =>
            {
                hostBuilder.ConfigureServices(services =>
                {
                    services.AddTransient(x => responseProvider);
                    _configureServices?.Invoke(services);
                });
            });
        }

        public virtual IHttpTestServer Create(IStatusCodeProvider statusCodeProvider, IResponseProvider responseProvider)
        {
            return Create(hostBuilder =>
            {
                hostBuilder.ConfigureServices(services =>
                {
                    services.AddTransient(x => statusCodeProvider);
                    services.AddTransient(x => responseProvider);
                    _configureServices?.Invoke(services);
                });
            });
        }

        public virtual IHttpTestServer Create<TStatusCodeProvider>(IResponseProvider responseProvider)
            where TStatusCodeProvider : class, IStatusCodeProvider
        {
            return Create(hostBuilder =>
            {
                hostBuilder.ConfigureServices(services =>
                {
                    services.AddTransient<IStatusCodeProvider, TStatusCodeProvider>();
                    services.AddTransient(x => responseProvider);
                    _configureServices?.Invoke(services);
                });
            });
        }

        public virtual IHttpTestServer Create<TResponseProvider>(IStatusCodeProvider statusCodeProvider)
            where TResponseProvider : class, IResponseProvider
        {
            return Create(hostBuilder =>
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
