using Microsoft.AspNetCore.Hosting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ForEvolve.Api.Contracts.Errors;
using System.Collections;
using System.Reflection;

namespace ForEvolve.AspNetCore.ErrorFactory.Implementations
{
    public class DefaultErrorFromExceptionFactoryTest
    {
        private readonly Mock<IHostingEnvironment> _hostingEnvironmentMock;
        private readonly Mock<IErrorFromRawValuesFactory> _errorFromRawValuesFactoryMock;
        private readonly DefaultErrorFromExceptionFactory _factoryUnderTest;

        public DefaultErrorFromExceptionFactoryTest()
        {
            _hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            _errorFromRawValuesFactoryMock = new Mock<IErrorFromRawValuesFactory>();
            _factoryUnderTest = new DefaultErrorFromExceptionFactory(
                _hostingEnvironmentMock.Object,
                _errorFromRawValuesFactoryMock.Object
            );
        }

        public class Ctor : DefaultErrorFromExceptionFactoryTest
        {
            [Fact]
            public void Should_guard_against_null()
            {
                // Arrange
                IHostingEnvironment nullEnvironmentDependency = null;
                IErrorFromRawValuesFactory nullFactoryDependency = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(
                    () => new DefaultErrorFromExceptionFactory(nullEnvironmentDependency, nullFactoryDependency)
                );
                Assert.Throws<ArgumentNullException>(
                    () => new DefaultErrorFromExceptionFactory(_hostingEnvironmentMock.Object, nullFactoryDependency)
                );
                Assert.Throws<ArgumentNullException>(
                    () => new DefaultErrorFromExceptionFactory(nullEnvironmentDependency, _errorFromRawValuesFactoryMock.Object)
                );
            }
        }

        public class CreateErrorCode : DefaultErrorFromExceptionFactoryTest
        {
            [Fact]
            public void Should_return_the_Exception_TypeName()
            {
                // Arrange
                var exception = new ForEvolveException();
                var expectedCode = nameof(ForEvolveException);

                // Act
                var result = _factoryUnderTest.CreateErrorCode(exception);

                // Assert
                Assert.Equal(expectedCode, result);
            }
        }

        public class CreateErrorSource : DefaultErrorFromExceptionFactoryTest
        {
            [Fact]
            public void Should_return_the_Exception_TargetSite_Name()
            {
                // Arrange
                ForEvolveException exception;
                try
                {
                    throw new ForEvolveException();
                }
                catch (ForEvolveException ex)
                {
                    exception = ex;
                }

                // Act
                var result = _factoryUnderTest.CreateErrorSource(exception);

                // Assert
                Assert.NotNull(exception.TargetSite);
                Assert.Equal(exception.TargetSite.Name, result);
            }

            [Fact]
            public void Should_return_the_Exception_Source_when_TargetSite_is_null()
            {
                // Arrange
                var expectedErrorSource = "SomeSource";
                var exception = new ForEvolveException
                {
                    Source = expectedErrorSource
                };

                // Act
                var result = _factoryUnderTest.CreateErrorSource(exception);

                // Assert
                Assert.Null(exception.TargetSite);
                Assert.Equal(expectedErrorSource, result);
            }

            public class TestException : Exception
            {
                public TestException(string targetSite, string source)
                    : base()
                {
                    //TargetSite = targetSite;

                }
            }
        }

        public class CreateDataErrorCode : DefaultErrorFromExceptionFactoryTest
        {
            [Fact]
            public void Should_return_the_specified_errorCode_suffixed_by_dot_Data()
            {
                // Arrange
                var errorCode = "SomeCode";
                var expectedErrorCode = "SomeCode.Data";

                // Act
                var result = _factoryUnderTest.CreateDataErrorCode(errorCode);

                // Assert
                Assert.Equal(expectedErrorCode, result);
            }
        }

        public class EnforceDetails : DefaultErrorFromExceptionFactoryTest
        {
            [Fact]
            public void Should_create_a_Details_list_when_Details_is_null()
            {
                // Arrange
                var error = new Error();

                // Act
                _factoryUnderTest.EnforceDetails(error);

                // Assert
                Assert.NotNull(error.Details);
            }

            [Fact]
            public void Should_not_override_Details_when_it_already_has_a_list()
            {
                // Arrange
                var details = new List<Error>();
                var error = new Error
                {
                    Details = details
                };

                // Act
                _factoryUnderTest.EnforceDetails(error);

                // Assert
                Assert.Same(details, error.Details);
            }
        }

        public class Create : DefaultErrorFromExceptionFactoryTest
        {
            [Theory]
            [InlineData(true, true)]
            [InlineData(true, false)]
            [InlineData(false, true)]
            [InlineData(false, false)]
            public void Should_return_an_Error_with_InnerError_HResult_and_StackTrace_set(bool isDevEnv, bool addData)
            {
                // Arrange
                SetupEnvironmentMock(isDevEnv);
                ForEvolveException exception;
                try
                {
                    throw new ForEvolveException();
                }
                catch (ForEvolveException ex)
                {
                    exception = ex;
                }

                if (addData)
                {
                    exception.Data.Add("Some", "Data");
                    exception.Data.Add("SomeMore", "UsefulData");
                }

                var expectedErrorCode = _factoryUnderTest.CreateErrorCode(exception);
                var expectedDetailsCode = _factoryUnderTest.CreateDataErrorCode(expectedErrorCode);
                var expectedError = new Error
                {
                    Code = expectedErrorCode,
                    Target = exception.TargetSite.Name,
                    Message = exception.Message,
                    InnerError = new InnerError
                    {
                        HResult = exception.HResult.ToString(),
                    }
                };
                if (isDevEnv)
                {
                    expectedError.InnerError.Source = exception.Source;
                    expectedError.InnerError.StackTrace = exception.StackTrace;
                    expectedError.InnerError.HelpLink = exception.HelpLink;
                    if (addData)
                    {
                        expectedError.Details = new List<Error>
                        {
                            new Error(), // Same as _errorFromRawValuesFactoryMock.Create(...)
                            new Error()
                        };
                    }
                }
                _errorFromRawValuesFactoryMock
                    .Setup(x => x.Create(expectedDetailsCode, It.IsAny<string>(), It.IsAny<object>()))
                    .Returns(new Error())
                    .Verifiable();

                // Act
                var result = _factoryUnderTest.Create(exception);

                // Assert
                if (isDevEnv)
                {
                    if (addData)
                    {
                        _errorFromRawValuesFactoryMock
                            .Verify(
                                x => x.Create(expectedDetailsCode, "Some", "Data"),
                                Times.Once
                            );
                        _errorFromRawValuesFactoryMock
                            .Verify(
                                x => x.Create(expectedDetailsCode, "SomeMore", "UsefulData"),
                                Times.Once
                            );
                    }
                }
                else
                {
                    _errorFromRawValuesFactoryMock
                        .Verify(
                            x => x.Create(expectedDetailsCode, It.IsAny<string>(), It.IsAny<object>()),
                            Times.Never
                        );
                }
                expectedError.AssertEqual(result);
            }

            [Fact]
            public void Should_return_an_Error_with_InnerException_as_Details()
            {
                // Arrange
                SetupEnvironmentMock(isDevEnv: false);
                ForEvolveException innerException;
                try
                {
                    throw new ForEvolveException();
                }
                catch (ForEvolveException ex)
                {
                    innerException = ex;
                }
                var exception = new Exception("Error!", innerException);

                // Act
                var result = _factoryUnderTest.Create(exception);

                // Assert
                Assert.Collection(result.Details,
                    (e) => Assert.Equal("ForEvolveException", e.Code)
                );
            }

            private void SetupEnvironmentMock(bool isDevEnv)
            {
                if (isDevEnv)
                {
                    _hostingEnvironmentMock
                        .Setup(x => x.EnvironmentName)
                        .Returns("Development");
                }
                else
                {
                    _hostingEnvironmentMock
                        .Setup(x => x.EnvironmentName)
                        .Returns("Production");
                }
            }
        }
    }
}
