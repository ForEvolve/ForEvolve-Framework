using ForEvolve.EntityFrameworkCore.Seeders.TestData;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.EntityFrameworkCore.Seeders
{
    public class SeederManagerFactoryTest
    {
        public class Ctor : SeederManagerFactoryTest
        {
            [Fact]
            public void Should_guard_against_null_serviceProvider()
            {
                // Arrange
                var nullServiceProvider = default(IServiceProvider);

                // Act & Assert
                Assert.Throws<ArgumentNullException>(
                    "serviceProvider",
                    () => new SeederManagerFactory(nullServiceProvider));
            }
        }

        public class Create : SeederManagerFactoryTest
        {
            [Fact]
            public void Should_return_the_expected_ISeederManager()
            {
                // Arrange
                var expectedSeederManager = new Mock<ISeederManager<SeederDbContext>>();
                var services = new ServiceCollection();
                services.AddScoped(sp => expectedSeederManager.Object);
                var serviceProvider = services.BuildServiceProvider();
                var sut = new SeederManagerFactory(serviceProvider);

                // Act
                var result = sut.Create<SeederDbContext>();

                // Assert
                Assert.Same(expectedSeederManager.Object, result);
            }

        }

        public class Dispose : SeederManagerFactoryTest
        {
            [Fact]
            public void Should_dispose_the_serviceScope()
            {
                // Arrange
                // The service scope
                var serviceScopeMock = new Mock<IServiceScope>();
                serviceScopeMock.Setup(x => x.Dispose()).Verifiable();

                // The scope factory (used internally by CreateScope)
                var serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();
                serviceScopeFactoryMock.Setup(x => x.CreateScope()).Returns(serviceScopeMock.Object);

                // The global service provider
                var serviceProviderMock = new Mock<IServiceProvider>();
                serviceProviderMock.Setup(x => x.GetService(typeof(IServiceScopeFactory))).Returns(serviceScopeFactoryMock.Object);

                // The subject under test
                var sut = new SeederManagerFactory(serviceProviderMock.Object);

                // Act
                sut.Dispose();

                // Assert
                serviceScopeMock.Verify(x => x.Dispose(), Times.Once);
            }

        }
    }
}
