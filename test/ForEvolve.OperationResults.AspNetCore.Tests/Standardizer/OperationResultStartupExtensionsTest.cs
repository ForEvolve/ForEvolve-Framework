using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.OperationResults.Standardizer
{
    [Collection(OperationResultStartupExtensionsServerCollection.Name)]
    public class OperationResultStartupExtensionsTest
    {
        private OperationResultStartupExtensionsServerFixture _server;
        public OperationResultStartupExtensionsTest(OperationResultStartupExtensionsServerFixture server)
        {
            _server = server ?? throw new ArgumentNullException(nameof(server));
        }

        [Fact]
        public async Task Should_standardize_OkObjectResult()
        {
            // Arrange
            var expectedBody = "{\"" + DefaultOperationResultStandardizerOptions.DefaultOperationName + "\":";
            expectedBody += "{\"messages\":[],\"succeeded\":true},";
            expectedBody += "\"someProp\":\"Oh Yeah!\",\"someOtherProp\":true}";

            // Act
            var result = await _server.Client.GetAsync("/OperationResultStartupExtensionsTestController/OkObjectResult");

            // Assert
            result.EnsureSuccessStatusCode();
            var body = await result.Content.ReadAsStringAsync();
            Assert.Equal(expectedBody, body);
        }

    }

    public class OperationResultStartupExtensionsServerFixture
    {
        public TestServer Server { get; }
        public HttpClient Client { get; }

        public OperationResultStartupExtensionsServerFixture()
        {
            //Action<IServiceCollection> configureServices
            //Action<IApplicationBuilder> configureApp
            var hostBuilder = WebHost.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddForEvolveOperationResultStandardizer();
                    services.AddMvc();
                })
                .Configure(app =>
                {
                    app.UseMvc();
                })
                ;
            Server = new TestServer(hostBuilder);
            Client = Server.CreateClient();
        }
    }

    [CollectionDefinition(Name)]
    public class OperationResultStartupExtensionsServerCollection : ICollectionFixture<OperationResultStartupExtensionsServerFixture>
    {
        public const string Name = "OperationResultStartupExtensions Server";
    }

    [Route("OperationResultStartupExtensionsTestController")]
    public class OperationResultStartupExtensionsTestController : ControllerBase
    {
        [HttpGet("OkObjectResult")]
        public IActionResult OkObjectResult()
        {
            var result = OperationResult.Success(new { SomeProp = "Oh Yeah!", SomeOtherProp = true });
            return Ok(result);
        }
    }
}
