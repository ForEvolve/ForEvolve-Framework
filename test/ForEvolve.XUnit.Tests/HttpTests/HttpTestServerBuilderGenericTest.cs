using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.XUnit.HttpTests
{
    public class HttpTestServerBuilderGenericTest
    {
        public class SimpleStartup
        {
            private readonly IHttpTestServerBuilder<BasicUseCaseStartup> _httpTestServerBuilder;
            public SimpleStartup()
            {
                _httpTestServerBuilder = new HttpTestServerBuilder<BasicUseCaseStartup>();
            }

            [Fact]
            public async Task Basic()
            {
                // Arrange
                using (var testServer = _httpTestServerBuilder.Create())
                {
                    // Act
                    var result = await testServer.Client.GetAsync("/whatever");

                    // Assert
                    Assert.Equal(StatusCodes.Status200OK, (int)result.StatusCode);
                }
            }

            [Fact]
            public async Task WithSomeCustomization()
            {
                // Arrange
                using (var testServer = _httpTestServerBuilder.Create(hostBuilder =>
                {
                    // Override the BasicUseCaseStartup.Configure method
                    hostBuilder.Configure(app =>
                    {
                        app.Run(context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status201Created;
                            return Task.CompletedTask;
                        });
                    });
                }))
                {
                    // Act
                    var result = await testServer.Client.GetAsync("/whatever");

                    // Assert
                    Assert.Equal(StatusCodes.Status201Created, (int)result.StatusCode);
                }
            }

            private class BasicUseCaseStartup
            {
                public void Configure(IApplicationBuilder app)
                {
                    app.Run(context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        return Task.CompletedTask;
                    });
                }
            }
        }

        public class TestServerStartupBuilder
        {
            private readonly ITestServerStartupHttpTestServerBuilder _httpTestServerBuilder;
            public TestServerStartupBuilder()
            {
                _httpTestServerBuilder = new TestServerStartupHttpTestServerBuilder();
            }

            public class RemovedFeature : TestServerStartupBuilder
            {
                [Fact]
                public void Arrange_with_no_argument_should_not_be_supported_with_TestServerStartup()
                {
                    // Arrange, Act & Assert
                    var result = Assert.Throws<NotSupportedException>(
                        () => _httpTestServerBuilder.Create()
                    );
                    Assert.Equal(
                        TestServerStartupHttpTestServerBuilder.ArrangeNotSupportedExceptionMessage,
                        result.Message
                    );
                }
            }

            public class Type : TestServerStartupBuilder
            {
                [Fact]
                public async Task With_StatusCodeProvider()
                {
                    // Arrange
                    using (var testServer = _httpTestServerBuilder.Create<ForbiddenStatusCodeProvider>())
                    {
                        // Act
                        var result = await testServer.Client.GetAsync("/whatever");

                        // Assert
                        Assert.Equal(ForbiddenStatusCodeProvider.ExpectedStatusCode, (int)result.StatusCode);
                    }
                }

                [Fact]
                public async Task With_StatusCodeProvider_and_ResponseProvider()
                {
                    // Arrange
                    using (var testServer = _httpTestServerBuilder.Create<CreatedStatusCodeProvider, CustomTestResponseProvider>())
                    {
                        // Act
                        var result = await testServer.Client.GetAsync("/whatever");

                        // Assert
                        Assert.Equal(CreatedStatusCodeProvider.ExpectedStatusCode, (int)result.StatusCode);
                        var responseContent = await result.Content.ReadAsStringAsync();
                        Assert.Equal("This is a response", responseContent);
                    }
                }
            }

            public class Implementation : TestServerStartupBuilder
            {
                [Fact]
                public async Task With_StatusCodeProvider()
                {
                    // Arrange
                    var statusCodeProvider = new CreatedStatusCodeProvider();
                    using (var testServer = _httpTestServerBuilder.Create(statusCodeProvider))
                    {
                        // Act
                        var result = await testServer.Client.GetAsync("/whatever");

                        // Assert
                        Assert.Equal(statusCodeProvider.StatusCode, (int)result.StatusCode);
                    }
                }

                [Fact]
                public async Task With_StatusCodeProvider_and_ResponseProvider()
                {
                    // Arrange
                    var statusCodeProvider = new CreatedStatusCodeProvider();
                    using (var testServer = _httpTestServerBuilder.Create(statusCodeProvider, new CustomTestResponseProvider()))
                    {
                        // Act
                        var result = await testServer.Client.GetAsync("/whatever");

                        // Assert
                        Assert.Equal(statusCodeProvider.StatusCode, (int)result.StatusCode);
                        var responseContent = await result.Content.ReadAsStringAsync();
                        Assert.Equal("This is a response", responseContent);
                    }
                }
            }

            public class MixedTypeAndImplementation : TestServerStartupBuilder
            {
                [Fact]
                public async Task With_ResponseProvider_type_and_StatusCodeProvider_implementation()
                {
                    // Arrange
                    var statusCodeProvider = new CreatedStatusCodeProvider();
                    using (var testServer = _httpTestServerBuilder.Create<CustomTestResponseProvider>(statusCodeProvider))
                    {
                        // Act
                        var result = await testServer.Client.GetAsync("/whatever");

                        // Assert
                        Assert.Equal(statusCodeProvider.StatusCode, (int)result.StatusCode);
                        var responseContent = await result.Content.ReadAsStringAsync();
                        Assert.Equal("This is a response", responseContent);
                    }
                }

                [Fact]
                public async Task With_StatusCodeProvider_type_and_ResponseProvider_implementation()
                {
                    // Arrange
                    using (var testServer = _httpTestServerBuilder.Create<CreatedStatusCodeProvider>(new CustomTestResponseProvider()))
                    {
                        // Act
                        var result = await testServer.Client.GetAsync("/whatever");

                        // Assert
                        Assert.Equal(CreatedStatusCodeProvider.ExpectedStatusCode, (int)result.StatusCode);
                        var responseContent = await result.Content.ReadAsStringAsync();
                        Assert.Equal("This is a response", responseContent);
                    }
                }
            }

            public sealed class CustomTestResponseProvider : IResponseProvider
            {
                public string ResponseText(HttpContext context)
                {
                    return "This is a response";
                }
            }
        }
    }

}
