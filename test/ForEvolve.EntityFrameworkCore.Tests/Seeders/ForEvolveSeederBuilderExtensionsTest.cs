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
    public class ForEvolveSeederBuilderExtensionsTest
    {
        public class Scan : ForEvolveSeederBuilderExtensionsTest
        {
            [Fact]
            public void Should_add_ISeeder_implementations_from_the_specified_assemblies()
            {
                // Arrange
                var services = new ServiceCollection();
                var builderMock = new Mock<IForEvolveSeederBuilder>();
                builderMock.Setup(x => x.Services).Returns(services);

                // Act
                builderMock.Object.Scan<TestEntitySeeder>();

                // Assert
                services.AssertTransientServiceExists<ISeeder<SeederDbContext>, TestEntitySeeder>();
            }

        }
    }
}
