using System;
using System.Linq;
using System.Collections.Generic;
using ForEvolve.Contracts.Errors;
using ForEvolve.DynamicInternalServerError.TWebServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.DynamicInternalServerError
{
    public class ModelStateValidationFilterTest
    {
        protected TestServer _server;
        protected HttpClient _client;

        public ModelStateValidationFilterTest()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
            );
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task SomeModelWithOneProperty_should_return_ErrorResponse()
        {
            // Arrange
            var model = new SomeModelWithOneProperty();

            // Act
            var response = await _client.PostAsync("/api/validate/oneproperty", model.ToJsonHttpContent());
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(responseString);
            var obj = JsonConvert.DeserializeObject<ErrorResponse>(responseString);
            Assert.NotNull(obj);
        }

        [Fact]
        public async Task SomeModelWithMultipleProperties_should_return_ErrorResponse()
        {
            // Arrange
            var model = new SomeModelWithMultipleProperties();

            // Act
            var response = await _client.PostAsync("/api/validate/multipleproperties", model.ToJsonHttpContent());
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(responseString);
            var obj = JsonConvert.DeserializeObject<ErrorResponse>(responseString);
            Assert.NotNull(obj);
        }
    }
}