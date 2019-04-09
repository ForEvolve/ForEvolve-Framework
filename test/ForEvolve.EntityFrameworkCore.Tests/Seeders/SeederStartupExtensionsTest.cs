using ForEvolve.EntityFrameworkCore.Seeders.TestData;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.EntityFrameworkCore.Seeders
{
    public class SeederStartupExtensionsTest
    {
        public class AddForEvolveSeeders : SeederStartupExtensionsTest
        {
            [Fact]
            public void Should_add_the_expected_services()
            {
                // Arrange
                var services = new ServiceCollection();

                // Act
                services.AddForEvolveSeeders();

                // Assert
                services.AssertSingletonServiceExists<ISeederManagerFactory, SeederManagerFactory>();
                services.AssertTransientServiceExists(typeof(ISeederManager<>), typeof(SeederManager<>));
            }

            [Fact]
            public void Should_return_a_ForEvolveSeederBuilder()
            {
                // Arrange
                var services = new ServiceCollection();

                // Act
                var builder = services.AddForEvolveSeeders();

                // Assert
                Assert.IsType<ForEvolveSeederBuilder>(builder);
            }
        }
    }
}
