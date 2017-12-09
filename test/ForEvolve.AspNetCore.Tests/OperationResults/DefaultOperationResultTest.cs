using ForEvolve.Api.Contracts.Errors;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
                _resultUnderTest.AddError(error);

                // Act
                var result = _resultUnderTest.Succeeded;

                // Assert
                Assert.False(result);
            }
        }

        public class AddError : DefaultOperationResultTest
        {
            [Fact]
            public void Should_guard_against_null_error()
            {
                // Arrange
                Error error = null;

                // Act & Assert
                var argEx = Assert.Throws<ArgumentNullException>(() => _resultUnderTest.AddError(error));
                Assert.Equal("error", argEx.ParamName);
            }

            [Fact]
            public void Should_add_errors_to_its_Errors_collection()
            {
                // Arrange
                var expectedError = CreateAnError();

                // Act
                _resultUnderTest.AddError(expectedError);

                // Assert
                Assert.Collection(_resultUnderTest.Errors,
                    error => Assert.Same(expectedError, error)
                );
            }
        }

        public class AddErrors : DefaultOperationResultTest
        {
            [Fact]
            public void Should_guard_against_null_error()
            {
                // Arrange
                IEnumerable<Error> errors = null;

                // Act & Assert
                var argEx = Assert.Throws<ArgumentNullException>(() => _resultUnderTest.AddErrors(errors));
                Assert.Equal("errors", argEx.ParamName);
            }

            [Fact]
            public void Should_add_errors_to_its_Errors_collection()
            {
                // Arrange
                var expectedErrors = new Error[] {
                    CreateAnError(),
                    CreateAnError(),
                    CreateAnError()
                };

                // Act
                _resultUnderTest.AddErrors(expectedErrors);

                // Assert
                Assert.Equal(_resultUnderTest.Errors, expectedErrors);
            }
        }

        public class AddErrorsFrom_IdentityResult : DefaultOperationResultTest
        {
            [Fact]
            public void Should_guard_against_null_result()
            {
                // Arrange
                IdentityResult result = null;

                // Act & Assert
                var argEx = Assert.Throws<ArgumentNullException>(() => _resultUnderTest.AddErrorsFrom(result));
                Assert.Equal("result", argEx.ParamName);
            }

            [Fact]
            public void Should_delegate_the_creation_to_IErrorFactory_and_add_errors_to_its_Errors_collection()
            {
                // Arrange
                var identityError = new IdentityError();
                var expectedErrors = new IdentityError[] { identityError };
                var result = IdentityResult.Failed(expectedErrors);
                var expectedError = CreateAnError();

                _errorFactoryMock
                    .Setup(x => x.Create(identityError))
                    .Returns(expectedError)
                    .Verifiable();

                // Act
                _resultUnderTest.AddErrorsFrom(result);

                // Assert
                _errorFactoryMock
                    .Verify(x => x.Create(identityError), Times.Once);
                Assert.Collection(
                    _resultUnderTest.Errors,
                    err => Assert.Same(expectedError, err)
                );
            }
        }

        public class AddErrorsFrom_IOperationResult : DefaultOperationResultTest
        {
            [Fact]
            public void Should_guard_against_null_result()
            {
                // Arrange
                IOperationResult result = null;

                // Act & Assert
                var argEx = Assert.Throws<ArgumentNullException>(() => _resultUnderTest.AddErrorsFrom(result));
                Assert.Equal("result", argEx.ParamName);
            }

            [Fact]
            public void Should_add_errors_to_its_Errors_collection()
            {
                // Arrange
                var resultMock = new Mock<IOperationResult>();
                var expectedErrors = new Error[] {
                    CreateAnError(),
                    CreateAnError()
                };
                resultMock
                    .Setup(x => x.Errors)
                    .Returns(expectedErrors);

                // Act
                _resultUnderTest.AddErrorsFrom(resultMock.Object);

                // Assert
                Assert.Equal(_resultUnderTest.Errors, expectedErrors);
            }

            [Fact]
            public void Should_add_exceptions_to_its_Errors_collection()
            {
                // Arrange
                var resultMock = new Mock<IOperationResult>();
                var expectedExceptions = new Exception[] {
                    new Exception(),
                    new Exception()
                };
                resultMock
                    .Setup(x => x.Exceptions)
                    .Returns(expectedExceptions);
                resultMock
                    .Setup(x => x.Errors)
                    .Returns(Enumerable.Empty<Error>());

                // Act
                _resultUnderTest.AddErrorsFrom(resultMock.Object);

                // Assert
                Assert.Equal(_resultUnderTest.Exceptions, expectedExceptions);
            }
        }

        public class AddException : DefaultOperationResultTest
        {
            [Fact]
            public void Should_guard_against_null_exception()
            {
                // Arrange
                Exception error = null;

                // Act & Assert
                var argEx = Assert.Throws<ArgumentNullException>(() => _resultUnderTest.AddException(error));
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
                _resultUnderTest.AddException(expectedException);

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
                _resultUnderTest.AddException(expectedException);

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
                _resultUnderTest.AddException(expectedException);

                // Assert
                _errorFactoryMock
                    .Verify(x => x.Create(expectedException), Times.Once);
            }
        }
    }
}
