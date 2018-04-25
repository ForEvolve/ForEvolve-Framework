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


        public class RequestScopeTest
        {
            [Fact]
            public void Services_should_return_the_same_singleton_object_everytime()
            {
                using (var sut = new MyHttpTest())
                {
                    var singleton1 = sut.Services.GetService<ISingletonDep>();
                    var singleton2 = sut.Services.GetService<ISingletonDep>();
                    Assert.Same(singleton1, singleton2);

                    using (var subScope = sut.Services.CreateScope())
                    {
                        var singleton3 = subScope.ServiceProvider.GetService<ISingletonDep>();
                        Assert.Same(singleton2, singleton3);
                    }
                }
            }

            [Fact]
            public void Services_should_return_the_same_scoped_object_in_the_current_scope()
            {
                using (var sut = new MyHttpTest())
                {
                    var scoped1 = sut.Services.GetService<IScopedDep>();
                    var scoped2 = sut.Services.GetService<IScopedDep>();
                    Assert.Same(scoped1, scoped2);

                    using (var subScope = sut.Services.CreateScope())
                    {
                        var scoped3 = subScope.ServiceProvider.GetService<IScopedDep>();
                        Assert.NotSame(scoped2, scoped3);
                    }
                }
            }

            [Fact]
            public void Services_should_always_return_a_different_transient_object_no_matter_the_scope()
            {
                using (var sut = new MyHttpTest())
                {
                    var transient1 = sut.Services.GetService<ITransientDep>();
                    var transient2 = sut.Services.GetService<ITransientDep>();
                    Assert.NotSame(transient1, transient2);

                    using (var subScope = sut.Services.CreateScope())
                    {
                        var transient3 = subScope.ServiceProvider.GetService<ITransientDep>();
                        Assert.NotSame(transient2, transient3);
                    }
                }
            }

            private interface ISingletonDep { }
            private interface IScopedDep { }
            private interface ITransientDep { }

            private class MyDependency : ISingletonDep, IScopedDep, ITransientDep { }

            private class MyHttpTest : BaseHttpTest
            {
                protected override IWebHostBuilder ConfigureWebHostBuilder(IWebHostBuilder webHostBuilder)
                {
                    webHostBuilder
                        .ConfigureServices(services =>
                        {
                            services
                                .AddSingleton<ISingletonDep, MyDependency>()
                                .AddScoped<IScopedDep, MyDependency>()
                                .AddTransient<ITransientDep, MyDependency>()
                                ;
                        })
                        .Configure(app => { })
                        ;
                    return webHostBuilder;
                }
            }
        }
    }
}
