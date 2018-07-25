using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.Extensions.Configuration
{
    public class InMemoryConfigurationBuilderExtensionsTest
    {
        public class AddKeyValue
        {
            [Fact]
            public void Should_associate_the_configuration_value_to_the_key()
            {
                // Arrange
                var configurationBuilder = new ConfigurationBuilder();

                // Act
                configurationBuilder.AddKeyValue("SomeKey", "SomeValue");

                // Assert
                var configurationRoot = configurationBuilder.Build();
                var value = configurationRoot.GetValue<string>("SomeKey");
                Assert.Equal("SomeValue", value);
            }
        }
    }
}
