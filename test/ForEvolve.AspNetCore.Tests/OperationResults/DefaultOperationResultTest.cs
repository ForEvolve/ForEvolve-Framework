using ForEvolve.Api.Contracts.Errors;
using Moq;
using System;
using Xunit;

namespace ForEvolve.AspNetCore.OperationResults
{
    public class DefaultOperationResultTest
    {
        private Mock<IErrorFactory> _errorFactoryMock;
        private DefaultOperationResult _resultUnderTest;

        public DefaultOperationResultTest()
        {
            // Arrange
            _errorFactoryMock = new Mock<IErrorFactory>();
            _resultUnderTest = new DefaultOperationResult(_errorFactoryMock.Object);
        }

        private static Error CreateAnError()
        {
            return new Error("someCode", "someMessage", "someTarget");
        }

        public class Ctor
        {
            [Fact]
            public void Should_guard_against_null()
            {
                // Arrange
                IErrorFactory nullFactory = null;

                // Act & Assert
                var argEx = Assert.Throws<ArgumentNullException>(
                    () => new DefaultOperationResult(nullFactory)
                );
                Assert.Equal("errorFactory", argEx.ParamName);
            }
        }

        public class Succeeded : DefaultOperationResultTest
        {
            [Fact]
            public void Should_return_true_when_there_is_no_error()
            {
                // Act
                var result = _resultUnderTest.Succeeded;

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void Should_return_true_when_there_is_an_error()
            {
                // Arrange
                var error = CreateAnError();
                _resultUnderTest.AppendError(error);

                // Act
                var result = _resultUnderTest.Succeeded;

                // Assert
                Assert.False(result);
            }
        }

        public class AppendError : DefaultOperationResultTest
        {
            [Fact]
            public void Should_guard_against_null_error()
            {
                // Arrange
                Error error = null;

                // Act & Assert
                var argEx = Assert.Throws<ArgumentNullException>(() => _resultUnderTest.AppendError(error));
                Assert.Equal("error", argEx.ParamName);
            }

            [Fact]
            public void Should_add_errors_to_its_Errors_collection()
            {
                // Arrange
                var expectedError = CreateAnError();

                // Act
                _resultUnderTest.AppendError(expectedError);

                // Assert
                Assert.Collection(_resultUnderTest.Errors,
                    error => Assert.Same(expectedError, error)
                );
            }
        }

        public class AppendException : DefaultOperationResultTest
        {
            [Fact]
            public void Should_guard_against_null_exception()
            {
                // Arrange
                Exception error = null;

                // Act & Assert
                var argEx = Assert.Throws<ArgumentNullException>(() => _resultUnderTest.AppendException(error));
                Assert.Equal("exception", argEx.ParamName);
            }

            [Fact]
            public void Should_add_exceptions_to_its_Errors_collection()
            {
                // Arrange
                var expectedException = new Exception();
                var expectedError = CreateAnError();
                _errorFactoryMock
                    .Setup(x => x.Create(expectedException))
                    .Returns(expectedError);

                // Act
                _resultUnderTest.AppendException(expectedException);

                // Assert
                Assert.Collection(
                    _resultUnderTest.Errors,
                    error => Assert.Same(expectedError, error)
                );
            }

            [Fact]
            public void Should_add_exceptions_to_its_Exceptions_collection()
            {
                // Arrange
                var expectedException = new Exception();
                _errorFactoryMock
                    .Setup(x => x.Create(expectedException))
                    .Returns(CreateAnError);

                // Act
                _resultUnderTest.AppendException(expectedException);

                // Assert
                Assert.True(_resultUnderTest.HasException());
                Assert.Collection(
                    _resultUnderTest.Exceptions,
                    exception => Assert.Same(expectedException, exception)
                );
            }

            [Fact]
            public void Should_delegate_the_exception_creation_to_its_underlying_IErrorFactory()
            {
                // Arrange
                var expectedException = new Exception();
                var expectedError = CreateAnError();
                _errorFactoryMock
                    .Setup(x => x.Create(expectedException))
                    .Returns(expectedError)
                    .Verifiable();

                // Act
                _resultUnderTest.AppendException(expectedException);

                // Assert
                _errorFactoryMock
                    .Verify(x => x.Create(expectedException), Times.Once);
            }
        }
    }
}
