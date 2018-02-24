using ForEvolve.Api.Contracts.Errors;
using ForEvolve.AspNetCore;
using ForEvolve.DynamicInternalServerError.TWebServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.DynamicInternalServerError
{
    public class MvcFiltersTest
    {
        protected TestServer _server;
        protected HttpClient _client;

        protected Exception _expectedException;

        public MvcFiltersTest(Exception expectedException)
        {
            // Arrange
            _expectedException = expectedException;
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureServices(x =>
                {
                    x.AddSingleton<Exception>(_expectedException);
                })
            );
            _client = _server.CreateClient();
        }

        public class SimpleException : MvcFiltersTest
        {
            public SimpleException()
                : base(new Exception("PipelineTest Exception"))
            {
            }

            [Fact]
            public async Task WebApi_should_return_InternalServerError()
            {
                // Act
                var response = await _client.GetAsync("/api/throw/exception");
                var responseString = await response.Content.ReadAsStringAsync();

                // Assert
                Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            }

            [Fact]
            public async Task WebApi_should_return_an_ErrorResponse()
            {
                // Arrange
                var errorBuilder = _server.Host.Services.GetService<IErrorFactory>();
                var expectedError = errorBuilder.CreateFrom(_expectedException);

                // Act
                var response = await _client.GetAsync("/api/throw/exception");
                var responseString = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseString);

                // Assert
                Assert.NotNull(errorResponse);
                Assert.NotNull(errorResponse.Error);
                Assert.Null(errorResponse.Error.Details);
                Assert.Null(errorResponse.Error.InnerError);
                Assert.Null(errorResponse.Error.Target);
                Assert.Equal(expectedError.Code, errorResponse.Error.Code);
                Assert.Equal(expectedError.Message, errorResponse.Error.Message);
            }
        }

        public class InnerException : MvcFiltersTest
        {
            public InnerException()
                : base(new Exception("PipelineTest Exception", new Exception("Inner exception message.")))
            {
            }

            [Fact]
            public async Task WebApi_should_return_an_ErrorResponse_including_details()
            {
                // Act
                var response = await _client.GetAsync("/api/throw/exception");
                var responseString = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseString);

                // Assert
                Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
                Assert.NotNull(errorResponse);
                Assert.NotNull(errorResponse.Error);
                Assert.Null(errorResponse.Error.InnerError);
                Assert.Null(errorResponse.Error.Target);
                Assert.Equal("Exception", errorResponse.Error.Code);
                Assert.Equal("PipelineTest Exception", errorResponse.Error.Message);

                // InnerException
                Assert.NotNull(errorResponse.Error.Details);
                Assert.Equal(1, errorResponse.Error.Details.Count);
                Assert.Equal("Exception", errorResponse.Error.Details[0].Code);
                Assert.Equal("Inner exception message.", errorResponse.Error.Details[0].Message);
                Assert.Null(errorResponse.Error.Details[0].InnerError);
                Assert.Null(errorResponse.Error.Details[0].Target);
            }
        }
    }
}
