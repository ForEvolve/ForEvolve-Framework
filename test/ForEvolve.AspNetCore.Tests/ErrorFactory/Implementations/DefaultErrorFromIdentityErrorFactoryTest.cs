using ForEvolve.Contracts.Errors;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ForEvolve.AspNetCore.ErrorFactory.Implementations
{
    public class DefaultErrorFromIdentityErrorFactoryTest
    {
        private readonly DefaultErrorFromIdentityErrorFactory _factoryUnderTest;
        private readonly Mock<IErrorFromRawValuesFactory> _errorFromRawValuesFactoryMock;
        public DefaultErrorFromIdentityErrorFactoryTest()
        {
            _errorFromRawValuesFactoryMock = new Mock<IErrorFromRawValuesFactory>();
            _factoryUnderTest = new DefaultErrorFromIdentityErrorFactory(_errorFromRawValuesFactoryMock.Object);
        }

        public class Ctor : DefaultErrorFromIdentityErrorFactoryTest
        {
            [Fact]
            public void Should_guard_against_null()
            {
                // Arrange
                IErrorFromRawValuesFactory nullErrorFromRawValuesFactory = null;

                // Act & Assert
                var argEx = Assert.Throws<ArgumentNullException>(() => new DefaultErrorFromIdentityErrorFactory(nullErrorFromRawValuesFactory));
                Assert.Equal("errorFromRawValuesFactory", argEx.ParamName);
            }
        }

        public class Create : DefaultErrorFromIdentityErrorFactoryTest
        {
            [Fact]
            public void Should_guard_against_null_error()
            {
                // Arrange
                IdentityError identityError = null;

                // Act & Assert
                var argEx = Assert.Throws<ArgumentNullException>(() => _factoryUnderTest.Create(identityError));
                Assert.Equal("identityError", argEx.ParamName);
            }

            [Fact]
            public void Should_delegate_the_creation_to_IErrorFromRawValuesFactory_with_Target_IdentityError()
            {
                // Arrange
                var expectedErrorCode = "Some error code";
                var expectedErrorTarget = nameof(IdentityError);
                var expectedErrorMessage = "Some error message";
                var identityError = new IdentityError {
                    Code = expectedErrorCode,
                    Description = expectedErrorMessage
                };
                var expectedError = new Error();
                _errorFromRawValuesFactoryMock
                    .Setup(x => x.Create(expectedErrorCode, expectedErrorTarget, expectedErrorMessage))
                    .Returns(expectedError)
                    .Verifiable();

                // Act
                var result = _factoryUnderTest.Create(identityError);

                // Assert
                _errorFromRawValuesFactoryMock.Verify(
                    x => x.Create(expectedErrorCode, expectedErrorTarget, expectedErrorMessage), 
                    Times.Once
                );
                Assert.Same(expectedError, result);
            }
        }
    }
}
