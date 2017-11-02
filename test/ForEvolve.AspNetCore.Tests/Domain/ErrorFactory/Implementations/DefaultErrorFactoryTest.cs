using ForEvolve.Api.Contracts.Errors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace ForEvolve.AspNetCore.ErrorFactory.Implementations
{
    public class DefaultErrorFactoryTest
    {
        [Fact(Skip = "DefaultErrorFactoryTest Should_be_Tested")]
        public void Should_be_Tested()
        {
            // Arrange


            // Act


            // Assert
            throw new NotImplementedException();
        }
    //    private readonly DefaultErrorFactory _factoryUnderTest;
    //    public DefaultErrorFactoryTest()
    //    {
    //        _factoryUnderTest = new DefaultErrorFactory();
    //    }

    //    private void AssertError(object expectedResult, Error actualResult)
    //    {
    //        var settings = new JsonSerializerSettings
    //        {
    //            NullValueHandling = NullValueHandling.Ignore
    //        };
    //        var expectedJson = JsonConvert.SerializeObject(expectedResult, settings);
    //        var actualJson = JsonConvert.SerializeObject(actualResult, settings);
    //        Assert.Equal(expectedJson, actualJson);
    //    }

    //    public class Create_with_errorCode : DefaultErrorFactoryTest
    //    {
    //        [Fact]
    //        public void Should_return_expected_Error()
    //        {
    //            // Arrange
    //            var errorCode = "MyErrorCode";
    //            var expectedError = new { code = errorCode };

    //            // Act
    //            var result = _factoryUnderTest.Create(errorCode);

    //            // Assert
    //            AssertError(expectedError, result);
    //        }
    //    }

    //    public class Create_with_errorCode_and_errorMessage : DefaultErrorFactoryTest
    //    {
    //        [Fact]
    //        public void Should_return_expected_Error()
    //        {
    //            // Arrange
    //            var errorCode = "MyErrorCode";
    //            var errorMessage = "MyErrorMessage ";
    //            var expectedError = new { code = errorCode, message = errorMessage };

    //            // Act
    //            var result = _factoryUnderTest.Create(errorCode, errorMessage);

    //            // Assert
    //            AssertError(expectedError, result);
    //        }
    //    }

    //    public class Create_with_errorCode_and_errorMessage_and_target : DefaultErrorFactoryTest
    //    {
    //        [Fact]
    //        public void Should_return_expected_Error()
    //        {
    //            // Arrange
    //            var errorCode = "MyErrorCode";
    //            var errorMessage = "MyErrorMessage ";
    //            var errorTarget = "MyTarget";
    //            var expectedError = new { code = errorCode, message = errorMessage, target = errorTarget };

    //            // Act
    //            var result = _factoryUnderTest.Create(errorCode, errorMessage, errorTarget);

    //            // Assert
    //            AssertError(expectedError, result);
    //        }
    //    }

    //    public class Create_TException : DefaultErrorFactoryTest
    //    {
    //        [Fact]
    //        public void Should_return_expected_Error()
    //        {
    //            // Arrange
    //            ArgumentException exception;
    //            try
    //            {
    //                throw new ArgumentException("My error message");
    //            }
    //            catch (ArgumentException ex)
    //            {
    //                exception = ex;
    //            }
    //            var expectedError = new Error
    //            {
    //                Code = "ArgumentException",
    //                Message = exception.Message,
    //                Target = exception.TargetSite?.Name
    //            };

    //            // Act
    //            var result = _factoryUnderTest.Create(exception);

    //            // Assert
    //            AssertError(expectedError, result);
    //        }

    //        [Fact]
    //        public void Should_return_expected_Error_including_InnerException()
    //        {
    //            // Arrange
    //            DivideByZeroException innerException;
    //            try
    //            {
    //                throw new DivideByZeroException();
    //            }
    //            catch (DivideByZeroException ex)
    //            {
    //                innerException = ex;
    //            }
    //            ArgumentException exception;
    //            try
    //            {
    //                throw new ArgumentException("My error message", innerException);
    //            }
    //            catch (ArgumentException ex)
    //            {
    //                exception = ex;
    //            }
    //            var expectedError = new Error
    //            {
    //                Code = "ArgumentException",
    //                Message = exception.Message,
    //                Target = exception.TargetSite?.Name,
    //                Details = new List<Error>
    //                {
    //                    new Error
    //                    {
    //                        Code = "DivideByZeroException",
    //                        Message = innerException.Message,
    //                        Target = innerException.TargetSite?.Name
    //                    }
    //                }
    //            };

    //            // Act
    //            var result = _factoryUnderTest.Create(exception);

    //            // Assert
    //            AssertError(expectedError, result);
    //        }
    //    }

    //    public class Create_TException_Detail_with_errorCode : DefaultErrorFactoryTest
    //    {
    //        [Fact]
    //        public void Should_return_expected_Error()
    //        {
    //            // Arrange
    //            var errorCode = "MyErrorCode";
    //            ArgumentException exception;
    //            try
    //            {
    //                throw new ArgumentException("My error message");
    //            }
    //            catch (ArgumentException ex)
    //            {
    //                exception = ex;
    //            }

    //            var expectedError = new Error
    //            {
    //                Code = errorCode,
    //                Details = new List<Error>
    //                {
    //                    new Error
    //                    {
    //                        Code = "ArgumentException",
    //                        Message = exception.Message,
    //                        Target = exception.TargetSite?.Name
    //                    }
    //                }
    //            };

    //            // Act
    //            var result = _factoryUnderTest.Create(errorCode, exception);

    //            // Assert
    //            AssertError(expectedError, result);
    //        }
    //    }

    //    public class Create_TException_Detail_with_errorCode_and_errorMessage : DefaultErrorFactoryTest
    //    {
    //        [Fact]
    //        public void Should_return_expected_Error()
    //        {
    //            // Arrange
    //            var errorCode = "MyErrorCode";
    //            var errorMessage = "MyErrorMessage ";
    //            ArgumentException exception;
    //            try
    //            {
    //                throw new ArgumentException("My error message");
    //            }
    //            catch (ArgumentException ex)
    //            {
    //                exception = ex;
    //            }

    //            var expectedError = new Error
    //            {
    //                Code = errorCode,
    //                Message = errorMessage,
    //                Details = new List<Error>
    //                {
    //                    new Error
    //                    {
    //                        Code = "ArgumentException",
    //                        Message = exception.Message,
    //                        Target = exception.TargetSite?.Name
    //                    }
    //                }
    //            };

    //            // Act
    //            var result = _factoryUnderTest.Create(errorCode, errorMessage, exception);

    //            // Assert
    //            AssertError(expectedError, result);
    //        }
    //    }

    //    public class Create_TException_Detail_with_errorCode_and_errorMessage_and_target : DefaultErrorFactoryTest
    //    {
    //        [Fact]
    //        public void Should_return_expected_Error()
    //        {
    //            // Arrange
    //            var errorCode = "MyErrorCode";
    //            var errorMessage = "MyErrorMessage ";
    //            var errorTarget = "MyTarget";
    //            ArgumentException exception;
    //            try
    //            {
    //                throw new ArgumentException("My error message");
    //            }
    //            catch (ArgumentException ex)
    //            {
    //                exception = ex;
    //            }

    //            var expectedError = new Error
    //            {
    //                Code = errorCode,
    //                Message = errorMessage,
    //                Target = errorTarget,
    //                Details = new List<Error>
    //                {
    //                    new Error
    //                    {
    //                        Code = "ArgumentException",
    //                        Message = exception.Message,
    //                        Target = exception.TargetSite?.Name
    //                    }
    //                }
    //            };

    //            // Act
    //            var result = _factoryUnderTest.Create(errorCode, errorMessage, errorTarget, exception);

    //            // Assert
    //            AssertError(expectedError, result);
    //        }
    //    }

    //    public class Create_Dictionary_Detail_with_errorCode : DefaultErrorFactoryTest
    //    {
    //        [Fact]
    //        public void Should_return_expected_Error()
    //        {
    //            // Arrange
    //            var errorCode = "MyErrorCode";
    //            var dictionary = new Dictionary<string, object>
    //            {
    //                { "MyKey1", "An error message" },
    //                { "MyKey2", "An other error message" },
    //            };
    //            var expectedError = new Error
    //            {
    //                Code = errorCode,
    //                Details = new List<Error>
    //                {
    //                    new Error
    //                    {
    //                        Code = $"{errorCode}Detail",
    //                        Message = "An error message",
    //                        Target = "MyKey1"
    //                    },
    //                    new Error
    //                    {
    //                        Code = $"{errorCode}Detail",
    //                        Message = "An other error message",
    //                        Target = "MyKey2"
    //                    },
    //                }
    //            };

    //            // Act
    //            var result = _factoryUnderTest.Create(errorCode, dictionary);

    //            // Assert
    //            AssertError(expectedError, result);
    //        }
    //    }

    //    public class Create_Dictionary_Detail_with_errorCode_and_errorMessage : DefaultErrorFactoryTest
    //    {
    //        [Fact]
    //        public void Should_return_expected_Error()
    //        {
    //            // Arrange
    //            var errorCode = "MyErrorCode";
    //            var errorMessage = "MyErrorMessage";
    //            var dictionary = new Dictionary<string, object>
    //            {
    //                { "MyKey1", "An error message" },
    //                { "MyKey2", "An other error message" },
    //            };
    //            var expectedError = new Error
    //            {
    //                Code = errorCode,
    //                Message = errorMessage,
    //                Details = new List<Error>
    //                {
    //                    new Error
    //                    {
    //                        Code = $"{errorCode}Detail",
    //                        Message = "An error message",
    //                        Target = "MyKey1"
    //                    },
    //                    new Error
    //                    {
    //                        Code = $"{errorCode}Detail",
    //                        Message = "An other error message",
    //                        Target = "MyKey2"
    //                    },
    //                }
    //            };

    //            // Act
    //            var result = _factoryUnderTest.Create(errorCode, errorMessage, dictionary);

    //            // Assert
    //            AssertError(expectedError, result);
    //        }
    //    }

    //    public class Create_Dictionary_Detail_with_errorCode_and_errorMessage_and_target : DefaultErrorFactoryTest
    //    {
    //        [Fact]
    //        public void Should_return_expected_Error()
    //        {
    //            // Arrange
    //            var errorCode = "MyErrorCode";
    //            var errorMessage = "MyErrorMessage";
    //            var errorTarget = "MyTarget";
    //            var dictionary = new Dictionary<string, object>
    //            {
    //                { "MyKey1", "An error message" },
    //                { "MyKey2", "An other error message" },
    //            };
    //            var expectedError = new Error
    //            {
    //                Code = errorCode,
    //                Message = errorMessage,
    //                Target = errorTarget,
    //                Details = new List<Error>
    //                {
    //                    new Error
    //                    {
    //                        Code = $"{errorCode}Detail",
    //                        Message = "An error message",
    //                        Target = "MyKey1"
    //                    },
    //                    new Error
    //                    {
    //                        Code = $"{errorCode}Detail",
    //                        Message = "An other error message",
    //                        Target = "MyKey2"
    //                    },
    //                }
    //            };

    //            // Act
    //            var result = _factoryUnderTest.Create(errorCode, errorMessage, errorTarget, dictionary);

    //            // Assert
    //            AssertError(expectedError, result);
    //        }
    //    }

    //    public class Create_KeyValuePair_with_errorCode : DefaultErrorFactoryTest
    //    {
    //        [Fact]
    //        public void Should_return_expected_Error()
    //        {
    //            // Arrange
    //            var errorCode = "MyErrorCode";
    //            var errorMessage = "MyErrorMessage";
    //            var errorTarget = "MyTarget";
    //            var errorTargetAndMessage = new KeyValuePair<string, object>(errorTarget, errorMessage);
    //            var expectedError = new Error
    //            {
    //                Code = errorCode,
    //                Message = errorMessage,
    //                Target = errorTarget
    //            };

    //            // Act
    //            var result = _factoryUnderTest.Create(errorCode, errorTargetAndMessage);

    //            // Assert
    //            throw new NotImplementedException();
    //        }

    //        [Fact]
    //        public void Should_return_expected_Error_and_details_when()
    //        {

    //        }
    //    }
    }
}
