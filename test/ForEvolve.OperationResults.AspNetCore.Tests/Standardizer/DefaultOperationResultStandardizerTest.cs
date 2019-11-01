using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ForEvolve.OperationResults.Standardizer
{
    public class DefaultOperationResultStandardizerTest
    {
        private readonly DefaultOperationResultStandardizer sut;

        private readonly Mock<IPropertyNameFormatter> _propertyNameFormatterMock;
        private readonly Mock<IPropertyValueFormatter> _propertyValueFormatterMock;
        private readonly DefaultOperationResultStandardizerOptions _options;
        private readonly Mock<IOptionsMonitor<DefaultOperationResultStandardizerOptions>> _optionsMock;
        private readonly ITestOutputHelper _output;

        public DefaultOperationResultStandardizerTest(ITestOutputHelper output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));

            _propertyNameFormatterMock = new Mock<IPropertyNameFormatter>();
            _propertyNameFormatterMock
                .Setup(x => x.Format(It.IsAny<string>()))
                .Returns((string input) => input);
            _propertyValueFormatterMock = new Mock<IPropertyValueFormatter>();
            _propertyValueFormatterMock
                .Setup(x => x.Format(It.IsAny<object>()))
                .Returns((object input) => input);
            _options = new DefaultOperationResultStandardizerOptions();
            _optionsMock = new Mock<IOptionsMonitor<DefaultOperationResultStandardizerOptions>>();
            _optionsMock.Setup(x => x.CurrentValue).Returns(_options);

            var logger = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.AddDebug();
                })
                .BuildServiceProvider()
                .GetService<ILoggerFactory>()
                .CreateLogger<DefaultOperationResultStandardizer>();

            sut = new DefaultOperationResultStandardizer(
                _propertyNameFormatterMock.Object,
                _propertyValueFormatterMock.Object,
                _optionsMock.Object,
                logger
            );
        }

        public class Standardize : DefaultOperationResultStandardizerTest
        {
            public Standardize(ITestOutputHelper output) : base(output) { }

            [Fact]
            public void Should_guard_against_null()
            {
                // Arrange
                var operationResult = default(IOperationResult);

                // Act & Assert
                Assert.Throws<ArgumentNullException>("operationResult", 
                    () => sut.Standardize(operationResult));
            }

            public class Given_an_IOperationResult : DefaultOperationResultStandardizerTest
            {
                public Given_an_IOperationResult(ITestOutputHelper output) : base(output) { }

                [Fact]
                public void Should_return_a_Dictionary_containing_only_the_OperationName_key()
                {
                    // Arrange
                    var operationResult = OperationResult.Success();
                    _options.OperationName = "op";

                    // Act
                    var result = sut.Standardize(operationResult);

                    // Assert
                    var dictionary = Assert.IsType<Dictionary<string, object>>(result);
                    Assert.Collection(dictionary,
                        keyValue =>
                        {
                            Assert.Equal("op", keyValue.Key);
                            Assert.NotNull(keyValue.Value);

                            var value = keyValue.Value;
                            value.Should().OwnProperty("Succeeded").That().Is().EqualTo(true);
                            value.Should().OwnProperty("Messages").That().Is().Empty();
                        }
                    );
                }
            }

            public class Given_an_IOperationResult_with_Value : DefaultOperationResultStandardizerTest
            {
                public Given_an_IOperationResult_with_Value(ITestOutputHelper output) : base(output) { }

                public class And_Given_an_anonymous_object : Given_an_IOperationResult_with_Value
                {
                    public And_Given_an_anonymous_object(ITestOutputHelper output) : base(output) { }

                    [Fact]
                    public void Should_return_a_Dictionary_containing_the_OperationName_key_and_the_Value_properties()
                    {
                        // Arrange
                        var expectedValue = new { SomeProp = "asdf", SomeOtherProp = true };
                        var operationResult = OperationResult.Success(expectedValue);
                        _options.OperationName = "op";

                        // Act
                        var result = sut.Standardize(operationResult);

                        // Assert
                        var dictionary = Assert.IsType<Dictionary<string, object>>(result);
                        Assert.Collection(dictionary,
                            keyValue =>
                            {
                                Assert.Equal("op", keyValue.Key);
                                Assert.NotNull(keyValue.Value);

                                var value = keyValue.Value;
                                value.Should().OwnProperty("Succeeded").That().Is().EqualTo(true);
                                value.Should().OwnProperty("Messages").That().Is().Empty();
                            },
                            keyValue =>
                            {
                                Assert.Equal("SomeProp", keyValue.Key);
                                keyValue.Value.Should().Be().EqualTo("asdf");
                            },
                            keyValue =>
                            {
                                Assert.Equal("SomeOtherProp", keyValue.Key);
                                keyValue.Value.Should().Be().EqualTo(true);
                            }
                        );
                    }
                }
                public class And_Given_an_typed_object : Given_an_IOperationResult_with_Value
                {
                    public And_Given_an_typed_object(ITestOutputHelper output) : base(output) { }

                    [Fact]
                    public void Should_return_a_Dictionary_containing_the_OperationName_key_and_the_Value_properties()
                    {
                        // Arrange
                        var expectedValue = new MyInternalTestObject
                        {
                            Name = "Old Man",
                            Age = 192
                        };
                        var operationResult = OperationResult.Success(expectedValue);
                        _options.OperationName = "op";

                        // Act
                        var result = sut.Standardize(operationResult);

                        // Assert
                        var dictionary = Assert.IsType<Dictionary<string, object>>(result);
                        Assert.Collection(dictionary,
                            keyValue =>
                            {
                                Assert.Equal("op", keyValue.Key);
                                Assert.NotNull(keyValue.Value);

                                var value = keyValue.Value;
                                value.Should().OwnProperty("Succeeded").That().Is().EqualTo(true);
                                value.Should().OwnProperty("Messages").That().Is().Empty();
                            },
                            keyValue =>
                            {
                                Assert.Equal("Name", keyValue.Key);
                                keyValue.Value.Should().Be().EqualTo("Old Man");
                            },
                            keyValue =>
                            {
                                Assert.Equal("Age", keyValue.Key);
                                keyValue.Value.Should().Be().EqualTo(192);
                            }
                        );
                    }

                    private class MyInternalTestObject
                    {
                        public string Name { get; set; }
                        public int Age { get; set; }
                    }
                }
            }
        }
    }
}
