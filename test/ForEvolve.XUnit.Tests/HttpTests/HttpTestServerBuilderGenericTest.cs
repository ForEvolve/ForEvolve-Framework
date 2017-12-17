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
            [Fact]
            public async Task Basic()
            {
                // This should be a class member and could be used in multiple tests
                IHttpTestServerBuilder<BasicUseCaseStartup> _httpTestServerBuilder = new HttpTestServerBuilder<BasicUseCaseStartup>();

                // Arrange
                using (var testServer = _httpTestServerBuilder.Arrange())
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
                // This should be a class member and could be used in multiple tests
                IHttpTestServerBuilder<BasicUseCaseStartup> _httpTestServerBuilder = new HttpTestServerBuilder<BasicUseCaseStartup>();

                // Arrange
                using (var testServer = _httpTestServerBuilder.Arrange(hostBuilder =>
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

        public class TestServerStartupManual
        {
            [Fact]
            public async Task Basic()
            {
                // This should be a class member and could be used in multiple tests
                IHttpTestServerBuilder<TestServerStartup> _httpTestServerBuilder = new HttpTestServerBuilder<TestServerStartup>();

                // Arrange
                using (var testServer = _httpTestServerBuilder.Arrange())
                {
                    // Act
                    var result = await testServer.Client.GetAsync("/whatever");

                    // Assert
                    Assert.Equal(StatusCodes.Status200OK, (int)result.StatusCode);
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

            public class Type : TestServerStartupBuilder
            {
                [Fact]
                public async Task With_StatusCodeProvider()
                {
                    // Arrange
                    using (var testServer = _httpTestServerBuilder.Arrange<ForbiddenStatusCodeProvider>())
                    {
                        // Act
                        var result = await testServer.Client.GetAsync("/whatever");

                        // Assert
                        Assert.Equal(ForbiddenStatusCodeProvider.ExpectedStatusCode, (int)result.StatusCode);
                    }
                }

                [Fact]
                public async Task With_ResponseProvider_and_StatusCodeProvider()
                {
                    // Arrange
                    using (var testServer = _httpTestServerBuilder.Arrange<CreatedStatusCodeProvider, CustomTestResponseProvider>())
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
                    using (var testServer = _httpTestServerBuilder.Arrange(statusCodeProvider))
                    {
                        // Act
                        var result = await testServer.Client.GetAsync("/whatever");

                        // Assert
                        Assert.Equal(statusCodeProvider.StatusCode, (int)result.StatusCode);
                    }
                }

                [Fact]
                public async Task With_ResponseProvider()
                {
                    // Arrange
                    using (var testServer = _httpTestServerBuilder.Arrange(new CustomTestResponseProvider()))
                    {
                        // Act
                        var result = await testServer.Client.GetAsync("/whatever");

                        // Assert
                        Assert.Equal(StatusCodes.Status200OK, (int)result.StatusCode);
                        var responseContent = await result.Content.ReadAsStringAsync();
                        Assert.Equal("This is a response", responseContent);
                    }
                }

                [Fact]
                public async Task With_ResponseProvider_and_StatusCodeProvider()
                {
                    // Arrange
                    var statusCodeProvider = new CreatedStatusCodeProvider();
                    using (var testServer = _httpTestServerBuilder.Arrange(statusCodeProvider, new CustomTestResponseProvider()))
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
                    using (var testServer = _httpTestServerBuilder.Arrange<CustomTestResponseProvider>(statusCodeProvider))
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
                public async Task With_ResponseProvider_implementation_and_StatusCodeProvider_type()
                {
                    // Arrange
                    using (var testServer = _httpTestServerBuilder.Arrange<CreatedStatusCodeProvider>(new CustomTestResponseProvider()))
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
