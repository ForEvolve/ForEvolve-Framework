using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.OperationResults.Standardizer
{
    public class DefaultOperationResultStandardizerTest
    {
        private readonly DefaultOperationResultStandardizer sut;

        private readonly Mock<IPropertyNameFormatter> _propertyNameFormatterMock;
        private readonly Mock<IPropertyValueFormatter> _propertyValueFormatterMock;
        private readonly DefaultOperationResultStandardizerOptions _options;
        private readonly Mock<IOptionsMonitor<DefaultOperationResultStandardizerOptions>> _optionsMock;

        public DefaultOperationResultStandardizerTest()
        {
            _propertyNameFormatterMock = new Mock<IPropertyNameFormatter>();
            _propertyValueFormatterMock = new Mock<IPropertyValueFormatter>();
            _options = new DefaultOperationResultStandardizerOptions();
            _optionsMock = new Mock<IOptionsMonitor<DefaultOperationResultStandardizerOptions>>();
            _optionsMock.Setup(x => x.CurrentValue).Returns(_options);

            sut = new DefaultOperationResultStandardizer(
                _propertyNameFormatterMock.Object,
                _propertyValueFormatterMock.Object,
                _optionsMock.Object
            );
        }

        public class Standardize : DefaultOperationResultStandardizerTest
        {
            [Fact]
            public void Should_guard_against_null()
            {
                // Arrange
                var operationResult = default(IOperationResult);

                // Act & Assert
                Assert.Throws<ArgumentNullException>("operationResult", 
                    () => sut.Standardize(operationResult));
            }

            public class Given_a_IOperationResult : DefaultOperationResultStandardizerTest
            {
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

            public class Given_a_IOperationResult_with_Value : DefaultOperationResultStandardizerTest
            {
                [Fact]
                public void Should_be_tested()
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
