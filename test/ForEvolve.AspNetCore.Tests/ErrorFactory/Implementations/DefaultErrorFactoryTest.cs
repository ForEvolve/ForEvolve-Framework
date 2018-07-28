using ForEvolve.Contracts.Errors;
using Microsoft.AspNetCore.Identity;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ForEvolve.AspNetCore.ErrorFactory.Implementations
{
    public class DefaultErrorFactoryTest
    {
        private readonly Mock<IErrorFromExceptionFactory> _errorFromExceptionFactoryMock;
        private readonly Mock<IErrorFromDictionaryFactory> _errorFromDictionaryFactoryMock;
        private readonly Mock<IErrorFromKeyValuePairFactory> _errorFromKeyValuePairFactoryMock;
        private readonly Mock<IErrorFromRawValuesFactory> _errorFromRawValuesFactoryMock;
        private readonly Mock<IErrorFromIdentityErrorFactory> _errorFromIdentityErrorFactoryMock;
        private readonly Mock<IErrorFromSerializableErrorFactory> _errorFromSerializableErrorFactoryMock;
        private readonly Mock<IErrorFromOperationResultFactory> _errorFromOperationResultFactoryMock;


        private readonly DefaultErrorFactory _factoryUnderTest;

        public DefaultErrorFactoryTest()
        {
            // Arrange dependencies
            _errorFromExceptionFactoryMock = new Mock<IErrorFromExceptionFactory>();
            _errorFromDictionaryFactoryMock = new Mock<IErrorFromDictionaryFactory>();
            _errorFromKeyValuePairFactoryMock = new Mock<IErrorFromKeyValuePairFactory>();
            _errorFromRawValuesFactoryMock = new Mock<IErrorFromRawValuesFactory>();
            _errorFromIdentityErrorFactoryMock = new Mock<IErrorFromIdentityErrorFactory>();
            _errorFromSerializableErrorFactoryMock = new Mock<IErrorFromSerializableErrorFactory>();
            _errorFromOperationResultFactoryMock = new Mock<IErrorFromOperationResultFactory>();

            // Arrange the test subject
            _factoryUnderTest = new DefaultErrorFactory(
                _errorFromExceptionFactoryMock.Object,
                _errorFromDictionaryFactoryMock.Object,
                _errorFromKeyValuePairFactoryMock.Object,
                _errorFromRawValuesFactoryMock.Object,
                _errorFromIdentityErrorFactoryMock.Object,
                _errorFromSerializableErrorFactoryMock.Object,
                _errorFromOperationResultFactoryMock.Object
            );
        }

        public class Ctor : DefaultErrorFactoryTest
        {
            [Fact]
            public void Should_guard_against_null()
            {
                // Arrange
                IErrorFromExceptionFactory nullErrorFromExceptionFactory = null;
                IErrorFromDictionaryFactory nullErrorFromDictionaryFactory = null;
                IErrorFromKeyValuePairFactory nullErrorFromKeyValuePairFactory = null;
                IErrorFromRawValuesFactory nullErrorFromRawValuesFactory = null;
                IErrorFromIdentityErrorFactory nullErrorFromIdentityErrorFactory = null;
                IErrorFromSerializableErrorFactory nullErrorFromSerializableErrorFactory = null;
                IErrorFromOperationResultFactory nullErrorFromOperationResultFactory = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(
                    () => new DefaultErrorFactory(
                        nullErrorFromExceptionFactory,
                        _errorFromDictionaryFactoryMock.Object,
                        _errorFromKeyValuePairFactoryMock.Object,
                        _errorFromRawValuesFactoryMock.Object,
                        _errorFromIdentityErrorFactoryMock.Object,
                        _errorFromSerializableErrorFactoryMock.Object,
                        _errorFromOperationResultFactoryMock.Object

                    )
                );
                Assert.Throws<ArgumentNullException>(
                    () => new DefaultErrorFactory(
                        _errorFromExceptionFactoryMock.Object,
                        nullErrorFromDictionaryFactory,
                        _errorFromKeyValuePairFactoryMock.Object,
                        _errorFromRawValuesFactoryMock.Object,
                        _errorFromIdentityErrorFactoryMock.Object,
                        _errorFromSerializableErrorFactoryMock.Object,
                        _errorFromOperationResultFactoryMock.Object
                    )
                );
                Assert.Throws<ArgumentNullException>(
                    () => new DefaultErrorFactory(
                        _errorFromExceptionFactoryMock.Object,
                        _errorFromDictionaryFactoryMock.Object,
                        nullErrorFromKeyValuePairFactory,
                        _errorFromRawValuesFactoryMock.Object,
                        _errorFromIdentityErrorFactoryMock.Object,
                        _errorFromSerializableErrorFactoryMock.Object,
                        _errorFromOperationResultFactoryMock.Object
                    )
                );
                Assert.Throws<ArgumentNullException>(
                    () => new DefaultErrorFactory(
                        _errorFromExceptionFactoryMock.Object,
                        _errorFromDictionaryFactoryMock.Object,
                        _errorFromKeyValuePairFactoryMock.Object,
                        nullErrorFromRawValuesFactory,
                        _errorFromIdentityErrorFactoryMock.Object,
                        _errorFromSerializableErrorFactoryMock.Object,
                        _errorFromOperationResultFactoryMock.Object
                    )
                );
                Assert.Throws<ArgumentNullException>(
                    () => new DefaultErrorFactory(
                        _errorFromExceptionFactoryMock.Object,
                        _errorFromDictionaryFactoryMock.Object,
                        _errorFromKeyValuePairFactoryMock.Object,
                        _errorFromRawValuesFactoryMock.Object,
                        nullErrorFromIdentityErrorFactory,
                        _errorFromSerializableErrorFactoryMock.Object,
                        _errorFromOperationResultFactoryMock.Object
                    )
                );
                Assert.Throws<ArgumentNullException>(
                    () => new DefaultErrorFactory(
                        _errorFromExceptionFactoryMock.Object,
                        _errorFromDictionaryFactoryMock.Object,
                        _errorFromKeyValuePairFactoryMock.Object,
                        _errorFromRawValuesFactoryMock.Object,
                        _errorFromIdentityErrorFactoryMock.Object,
                        nullErrorFromSerializableErrorFactory,
                        _errorFromOperationResultFactoryMock.Object
                    )
                );
                Assert.Throws<ArgumentNullException>(
                    () => new DefaultErrorFactory(
                        _errorFromExceptionFactoryMock.Object,
                        _errorFromDictionaryFactoryMock.Object,
                        _errorFromKeyValuePairFactoryMock.Object,
                        _errorFromRawValuesFactoryMock.Object,
                        _errorFromIdentityErrorFactoryMock.Object,
                        _errorFromSerializableErrorFactoryMock.Object,
                        nullErrorFromOperationResultFactory
                    )
                );
            }
        }

        public class Create : DefaultErrorFactoryTest
        {
            public class ErrorFromException : Create
            {
                [Fact]
                public void Should_delegate_the_call_to_IErrorFromExceptionFactory()
                {
                    // Arrange
                    var expectedError = new Error();
                    var exception = new ForEvolveException();
                    _errorFromExceptionFactoryMock
                        .Setup(x => x.CreateFrom(exception))
                        .Returns(expectedError);

                    // Act
                    var result = _factoryUnderTest.CreateFrom(exception);

                    // Assert
                    Assert.Same(expectedError, result);
                }
            }

            public class ErrorFromDictionary : Create
            {
                [Fact]
                public void Should_delegate_the_call_to_IErrorFromDictionaryFactory()
                {
                    // Arrange
                    var expectedErrors = Enumerable.Empty<Error>();
                    var errorCode = "SomeCode";
                    Dictionary<string, object> details = new Dictionary<string, object>();

                    _errorFromDictionaryFactoryMock
                        .Setup(x => x.Create(errorCode, details))
                        .Returns(expectedErrors);

                    // Act
                    var result = _factoryUnderTest.Create(errorCode, details);

                    // Assert
                    Assert.Same(expectedErrors, result);
                }
            }

            public class ErrorFromKeyValuePair : Create
            {
                [Fact]
                public void Should_delegate_the_call_to_IErrorFromKeyValuePairFactory()
                {
                    // Arrange
                    var expectedError = new Error();
                    var errorCode = "SomeCode";
                    var keyValuePair = new KeyValuePair<string, object>("...", "...");
                    _errorFromKeyValuePairFactoryMock
                        .Setup(x => x.Create(errorCode, keyValuePair))
                        .Returns(expectedError);

                    // Act
                    var result = _factoryUnderTest.Create(errorCode, keyValuePair);

                    // Assert
                    Assert.Same(expectedError, result);
                }
            }

            public class ErrorFromRawValues : Create
            {
                [Fact]
                public void Should_delegate_the_call_to_IErrorFromRawValuesFactory()
                {
                    // Arrange
                    var expectedError = new Error();
                    var errorCode = "someCode";
                    var errorTarget = "someTarget";
                    var errorMessage = "someMessage";

                    _errorFromRawValuesFactoryMock
                        .Setup(x => x.Create(errorCode, errorTarget, errorMessage))
                        .Returns(expectedError);

                    // Act
                    var result = _factoryUnderTest.Create(errorCode, errorTarget, errorMessage);

                    // Assert
                    Assert.Same(expectedError, result);
                }
            }

            public class ErrorFromIdentityError : Create
            {
                [Fact]
                public void Should_delegate_the_call_to_IErrorFromIdentityErrorFactory()
                {
                    // Arrange
                    var expectedError = new Error();
                    var identityError = new IdentityError();

                    _errorFromIdentityErrorFactoryMock
                        .Setup(x => x.Create(identityError))
                        .Returns(expectedError);

                    // Act
                    var result = _factoryUnderTest.Create(identityError);

                    // Assert
                    Assert.Same(expectedError, result);
                }
            }
        }
    }
}
