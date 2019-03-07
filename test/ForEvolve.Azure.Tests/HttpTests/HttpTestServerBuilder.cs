using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Net.Http;

namespace ForEvolve.XUnit.HttpTests
{
    [Obsolete(ObsoleteMessage.Xunit, false)]
    public class HttpTestServerBuilder : IHttpTestServerBuilder
    {
        public virtual IHttpTestServer Create(Func<IWebHostBuilder> webHostBuilderImplementationFactory)
        {
            TestServer server = null;
            HttpClient client = null;
            var builder = webHostBuilderImplementationFactory();
            builder.ConfigureServices(services =>
            {
                services.TryAddSingleton<IHttpTestServer, HttpTestServer>();
                services.TryAddSingleton(x => client);
                services.TryAddSingleton(x => server);
            });
            server = new TestServer(builder);
            client = server.CreateClient();
            client.BaseAddress = new Uri("http://localhost");

            var testServer = server.Host.Services.GetRequiredService<IHttpTestServer>();
            return testServer;
        }
    }

    [Obsolete(ObsoleteMessage.Xunit, false)]
    public class HttpTestServerBuilder<TStartup> : HttpTestServerBuilder, IHttpTestServerBuilder<TStartup>
         where TStartup : class
    {
        public virtual IHttpTestServer Create()
        {
            return Create(hostBuilder => { });
        }

        public virtual IHttpTestServer Create(Action<IWebHostBuilder> webHostBuilderSetup)
        {
            var hostBuilder = WebHost
                .CreateDefaultBuilder()
                .UseStartup<TStartup>();
            webHostBuilderSetup(hostBuilder);
            return Create(() => hostBuilder);
        }
    }
}
