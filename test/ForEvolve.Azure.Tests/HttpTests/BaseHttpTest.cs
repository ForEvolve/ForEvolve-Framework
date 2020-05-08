using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Builder;
using System.IO;
using System.Reflection;

namespace ForEvolve.XUnit.HttpTests
{
    [Obsolete(ObsoleteMessage.Xunit, false)]
    public abstract class BaseHttpTestWithMvcViews<TStartup> : BaseHttpTest<TStartup>
        where TStartup : class
    {

        protected override IWebHostBuilder ConfigureWebHostBuilder(IWebHostBuilder webHostBuilder)
        {
            var contentRoot = GetContentRoot();
            return base.ConfigureWebHostBuilder(webHostBuilder
                .UseContentRoot(contentRoot)
            );
        }

        protected virtual string GetContentRoot()
        {
            var startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;
            var startupAssemblyName = startupAssembly.GetName().Name;
            var contentRoot = Path.GetFullPath($"{SrcRoot}/{startupAssemblyName}");
            return contentRoot;
        }

        protected virtual string SrcRoot => "../../../../../src";
    }

    [Obsolete(ObsoleteMessage.Xunit, false)]
    public abstract class BaseHttpTest<TStartup> : BaseHttpTest
         where TStartup : class
    {
        public BaseHttpTest()
            : base(WebHost.CreateDefaultBuilder().UseStartup<TStartup>())
        {
        }
    }

    [Obsolete(ObsoleteMessage.Xunit, false)]
    public abstract class BaseHttpTest : IDisposable
    {
        protected TestServer Server => _httpTestServer.Server;
        protected HttpClient Client => _httpTestServer.Client;

        protected virtual string Environment => "Development";

        private readonly IHttpTestServer _httpTestServer;

        public IServiceProvider Services => RequestScope.ServiceProvider;
        protected IServiceScope RequestScope { get; }

        public BaseHttpTest()
            : this(WebHost.CreateDefaultBuilder())
        {
        }

        public BaseHttpTest(IWebHostBuilder webHostBuilder)
        {
            if (webHostBuilder == null) { throw new ArgumentNullException(nameof(webHostBuilder)); }

            var builder = webHostBuilder
                .UseEnvironment(Environment)
                .ConfigureServices(ConfigureServices)
                .ConfigureServices(services => services.TryAddTransient<IStatusCodeProvider, OkStatusCodeProvider>())
                ;
            builder = ConfigureWebHostBuilder(builder);
            _httpTestServer = new HttpTestServerBuilder().Create(() => builder);

            // Create a request scope
            RequestScope = Server.Host.Services.CreateScope();
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
        }

        protected virtual IWebHostBuilder ConfigureWebHostBuilder(IWebHostBuilder webHostBuilder)
        {
            return webHostBuilder;
        }

        #region IDisposable Support

        private bool _isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    RequestScope.Dispose();
                    _httpTestServer.Dispose();
                }
                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
