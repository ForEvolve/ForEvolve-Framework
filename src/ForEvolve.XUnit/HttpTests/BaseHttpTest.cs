using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace ForEvolve.XUnit.HttpTests
{
    public abstract class BaseHttpTest<TStartup> : BaseHttpTest
         where TStartup : class
    {
        public BaseHttpTest()
            : base(WebHost.CreateDefaultBuilder().UseStartup<TStartup>())
        {
        }
    }

    public abstract class BaseHttpTest : IDisposable
    {
        protected TestServer Server { get; }
        protected HttpClient Client { get; }

        protected virtual Uri BaseAddress => new Uri("http://localhost");
        protected virtual string Environment => "Development";

        public BaseHttpTest()
            : this(WebHost.CreateDefaultBuilder())
        {
        }

        public BaseHttpTest(IWebHostBuilder webHostBuilder)
        {
            if (webHostBuilder == null) { throw new ArgumentNullException(nameof(webHostBuilder)); }

            var builder = webHostBuilder
                .UseEnvironment(Environment)
                .ConfigureServices(ConfigureServices);

            Server = new TestServer(builder);
            Client = Server.CreateClient();
            Client.BaseAddress = BaseAddress;
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
        }

        #region IDisposable Support

        private bool _isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    Client.Dispose();
                    Server.Dispose();
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
