using Microsoft.Extensions.DependencyInjection;

namespace ForEvolve.XUnit.HttpTests
{
    public class TestServerStartupHttpTestServerBuilder : HttpTestServerBuilder<TestServerStartup>, ITestServerStartupHttpTestServerBuilder
    {
        public IHttpTestServer Arrange<TStatusCodeProvider>()
            where TStatusCodeProvider : class, IStatusCodeProvider
        {
            return Arrange(hostBuilder =>
            {
                hostBuilder.ConfigureServices(services =>
                {
                    services.AddTransient<IStatusCodeProvider, TStatusCodeProvider>();
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
                });
            });
        }
    }
}
