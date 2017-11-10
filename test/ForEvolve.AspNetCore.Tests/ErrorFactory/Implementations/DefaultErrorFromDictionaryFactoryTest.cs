using ForEvolve.Api.Contracts.Errors;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ForEvolve.AspNetCore.ErrorFactory.Implementations
{
    public class DefaultErrorFromDictionaryFactoryTest
    {
        public class Ctor
        {
            [Fact]
            public void Should_guard_against_null()
            {
                // Arrange
                IErrorFromKeyValuePairFactory nullDependency = null;

                // Act & Assert
                var exception = Assert.Throws<ArgumentNullException>(
                    () => new DefaultErrorFromDictionaryFactory(nullDependency)
                );
            }
        }

        public class Create
        {
            [Fact]
            public void Should_delegate_the_Error_creation_to_IErrorFromRawValuesFactory()
            {
                // Arrange
                var expectedErrorCode = "SomeErrorCode";
                var keyValues = new KeyValuePair<string, object>[]{
                    new KeyValuePair<string, object>("SomeErrorTarget1", "SomeErrorMessage1"),
                    new KeyValuePair<string, object>("SomeErrorTarget2", "SomeErrorMessage2"),
                    new KeyValuePair<string, object>("SomeErrorTarget3", "SomeErrorMessage3")
                };
                var errors = new Dictionary<string, object>(keyValues);

                var keyValueFactoryMock = new Mock<IErrorFromKeyValuePairFactory>();
                var factoryUnderTest = new DefaultErrorFromDictionaryFactory(keyValueFactoryMock.Object);
                keyValueFactoryMock
                    .Setup(x => x.Create(expectedErrorCode, It.IsIn(keyValues)))
                    .Verifiable();

                // Act
                var result = factoryUnderTest.Create(expectedErrorCode, errors)
                    // Trigger the ondemand operation created by Select(...) 
                    // so the mock is invoked
                    .ToArray(); 

                // Assert
                keyValueFactoryMock.Verify(
                    x => x.Create(expectedErrorCode, It.IsIn(keyValues)),
                    Times.Exactly(errors.Count)
                );
            }
        }
    }
}
