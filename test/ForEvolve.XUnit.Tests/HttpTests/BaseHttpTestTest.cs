using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.XUnit.HttpTests
{
    public class BaseHttpTestTest : BaseHttpTest<Startup>
    {
        [Fact]
        public async Task Should_setup_the_client_and_the_server()
        {
            // Arrange
            var expectedStatusCode = Startup.DefaultStatusCode;

            // Act
            var result = await Client.GetAsync("/");

            // Assert
            Assert.Equal(expectedStatusCode, (int)result.StatusCode);
        }

        public class ConfigureServicesTest : BaseHttpTest<Startup>
        {
            private const int ExpectedStatusCode = StatusCodes.Status202Accepted;

            [Fact]
            public async Task Should_setup_the_client_and_the_server()
            {
                // Arrange
                var expectedStatusCode = ExpectedStatusCode;

                // Act
                var result = await Client.GetAsync("/");

                // Assert
                Assert.Equal(expectedStatusCode, (int)result.StatusCode);
            }

            protected override void ConfigureServices(IServiceCollection services)
            {
                services.TryAddSingleton<IStatusCodeProvider>(new StatusCodeProvider(ExpectedStatusCode));
            }
        }

    }

    public class Startup
    {
        public const int DefaultStatusCode = StatusCodes.Status200OK;

        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IStatusCodeProvider>(new StatusCodeProvider(DefaultStatusCode));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Run(context =>
            {
                var statusCodeProvider = app.ApplicationServices
                    .GetRequiredService<IStatusCodeProvider>();
                context.Response.StatusCode = statusCodeProvider.StatusCode;
                return context.Response.WriteAsync(context.Request.Path);
            });
        }
    }

    public interface IStatusCodeProvider
    {
        int StatusCode { get; }
    }

    public class StatusCodeProvider : IStatusCodeProvider
    {
        public StatusCodeProvider(int statusCode)
        {
            StatusCode = statusCode;
        }
        public int StatusCode { get; }
    }

}
