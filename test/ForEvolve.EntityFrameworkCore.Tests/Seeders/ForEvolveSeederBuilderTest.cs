using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.EntityFrameworkCore.Seeders
{
    public class ForEvolveSeederBuilderTest
    {
        public class Ctor : ForEvolveSeederBuilderTest
        {
            [Fact]
            public void Should_guard_against_null()
            {
                // Arrange
                var nullServiceCollection = default(IServiceCollection);

                // Act & Assert
                Assert.Throws<ArgumentNullException>(
                    "services",
                    () => new ForEvolveSeederBuilder(nullServiceCollection));
            }

            [Fact]
            public void Should_set_the_services()
            {
                // Arrange
                var services = new ServiceCollection();

                // Act
                var sut = new ForEvolveSeederBuilder(services);

                // Assert
                Assert.Same(services, sut.Services);
            }
        }
    }
}
