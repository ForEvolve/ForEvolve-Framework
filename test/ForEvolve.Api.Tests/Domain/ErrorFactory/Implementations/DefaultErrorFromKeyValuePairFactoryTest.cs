using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ForEvolve.Api.Domain.ErrorFactory.Implementations
{
    public class DefaultErrorFromKeyValuePairFactoryTest
    {
        public class Ctor
        {
            [Fact]
            public void Should_guard_against_null()
            {
                // Arrange
                IErrorFromRawValuesFactory nullDependency = null;

                // Act & Assert
                var exception = Assert.Throws<ArgumentNullException>(
                    () => new DefaultErrorFromKeyValuePairFactory(nullDependency)
                );
            }
        }

        public class Create
        {
            [Fact]
            public void Should_delegate_the_Error_creation_to_IErrorFromRawValuesFactory()
            {
                // Arrange
                var expectedEerrorCode = "SomeErrorCode";
                var expectedErrorTarget = "SomeErrorTarget";
                var expectedErrorMessage = "SomeErrorMessage";
                var errorTargetAndMessage = new KeyValuePair<string, object>(expectedErrorTarget, expectedErrorMessage);

                var rawFactoryMock = new Mock<IErrorFromRawValuesFactory>();
                var factoryUnderTest = new DefaultErrorFromKeyValuePairFactory(rawFactoryMock.Object);
                rawFactoryMock
                    .Setup(x => x.Create(expectedEerrorCode, expectedErrorTarget, expectedErrorMessage))
                    .Verifiable();

                // Act
                var result = factoryUnderTest.Create(expectedEerrorCode, errorTargetAndMessage);

                // Assert
                rawFactoryMock.Verify(x => x.Create(expectedEerrorCode, expectedErrorTarget, expectedErrorMessage), Times.Once);
            }
        }
    }
}
