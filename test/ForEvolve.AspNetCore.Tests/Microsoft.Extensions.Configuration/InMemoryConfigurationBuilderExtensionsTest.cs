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

        public class AddDictionary
        {
            [Fact]
            public void Should_associate_the_configuration_values_to_their_key()
            {
                // Arrange
                var configurationBuilder = new ConfigurationBuilder();
                var data = new Dictionary<string, string>
                {
                    { "Key1", "Value 1" },
                    { "Key2", "Value 2" },
                    { "Key3", "Value 3" },
                };

                // Act
                configurationBuilder.AddDictionary(data);

                // Assert
                var configurationRoot = configurationBuilder.Build();
                var value1 = configurationRoot.GetValue<string>("Key1");
                var value2 = configurationRoot.GetValue<string>("Key2");
                var value3 = configurationRoot.GetValue<string>("Key3");
                Assert.Equal("Value 1", value1);
                Assert.Equal("Value 2", value2);
                Assert.Equal("Value 3", value3);
            }

            [Fact]
            public void Should_support_complex_object()
            {
                // Arrange
                var configurationBuilder = new ConfigurationBuilder();
                var data = new Dictionary<string, string>
                {
                    { "SomeSection:Id", "1" },
                    { "SomeSection:Name", "My name" },
                };

                // Act
                configurationBuilder.AddDictionary(data);

                // Assert
                var configurationRoot = configurationBuilder.Build();
                var value = configurationRoot
                    .GetSection("SomeSection")
                    .Get<MytestObject>();

                Assert.NotNull(value);
                Assert.Equal(1, value.Id);
                Assert.Equal("My name", value.Name);
            }

            private class MytestObject
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }
        }
    }
}
