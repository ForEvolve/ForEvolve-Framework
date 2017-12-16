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
    public class BaseHttpTestTest : BaseHttpTest<TestServerStartup>
    {
        [Fact]
        public async Task Should_setup_the_client_and_the_server()
        {
            // Arrange
            var expectedStatusCode = StatusCodes.Status200OK;

            // Act
            var result = await Client.GetAsync("/");

            // Assert
            Assert.Equal(expectedStatusCode, (int)result.StatusCode);
        }

        public class ConfigureServicesTest : BaseHttpTest<TestServerStartup>
        {
            private const int ExpectedStatusCode = StatusCodes.Status201Created;

            protected override void ConfigureServices(IServiceCollection services)
            {
                // Arrange
                services.TryAddSingleton<IStatusCodeProvider, CreatedStatusCodeProvider>();
            }

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
        }

    }
}
