using ForEvolve.Api.Contracts.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Api.Domain
{
    public class DefaultErrorFromRawValuesFactoryTest
    {
        private readonly DefaultErrorFromRawValuesFactory _factoryUnderTest;

        public DefaultErrorFromRawValuesFactoryTest()
        {
            _factoryUnderTest = new DefaultErrorFromRawValuesFactory();
        }

        public class Create : DefaultErrorFromRawValuesFactoryTest
        {
            public class WhenErrorMessageIsAString : Create
            {
                [Fact]
                public void Should_return_a_simple_Error_object()
                {
                    // Arrange
                    var errorCode = "SomeErrorCode";
                    var errorTarget = "SomeErrorTarget";
                    var errorMessage = "SomeString";
                    var expectedError = new Error
                    {
                        Code = errorCode,
                        Target = errorTarget,
                        Message = errorMessage
                    };

                    // Act
                    var result = _factoryUnderTest.Create(errorCode, errorTarget, errorMessage);

                    // Assert
                    expectedError.AssertEqual(result);
                }
            }
            public class WhenErrorMessageIsAStringEnumerable : Create
            {
                public class ThatIsEmpty : WhenErrorMessageIsAStringEnumerable
                {
                    [Fact]
                    public void Should_return_a_simple_Error_object_with_a_code_and_a_target()
                    {
                        // Arrange
                        var errorCode = "SomeErrorCode";
                        var errorTarget = "SomeErrorTarget";
                        var errorMessage = Enumerable.Empty<string>();
                        var expectedError = new Error
                        {
                            Code = errorCode,
                            Target = errorTarget
                        };

                        // Act
                        var result = _factoryUnderTest.Create(errorCode, errorTarget, errorMessage);

                        // Assert
                        expectedError.AssertEqual(result);
                    }
                }

                public class ContainingOneItem : WhenErrorMessageIsAStringEnumerable
                {
                    [Fact]
                    public void Should_return_a_simple_Error_object()
                    {
                        // Arrange
                        var errorCode = "SomeErrorCode";
                        var errorTarget = "SomeErrorTarget";
                        var errorMessage = new[] { "SomeString" };
                        var expectedError = new Error
                        {
                            Code = errorCode,
                            Target = errorTarget,
                            Message = "SomeString"
                        };

                        // Act
                        var result = _factoryUnderTest.Create(errorCode, errorTarget, errorMessage);

                        // Assert
                        expectedError.AssertEqual(result);
                    }
                }

                public class ContainingMultipleItems : WhenErrorMessageIsAStringEnumerable
                {
                    [Fact]
                    public void Should_return_an_Error_object_with_no_Message_and_with_a_Detail_Error_object_per_element()
                    {
                        // Arrange
                        var errorCode = "SomeErrorCode";
                        var expectedDetailsErrorCode = $"{errorCode}Message";
                        var errorTarget = "SomeErrorTarget";
                        var errorMessage = new[] { "SomeString", "SomeOtherString", "AThirdString" };
                        var expectedError = new Error
                        {
                            Code = errorCode,
                            Target = errorTarget,
                            Details = new List<Error>
                            {
                                new Error {
                                    Code = expectedDetailsErrorCode,
                                    Message = "SomeString"
                                },
                                new Error {
                                    Code = expectedDetailsErrorCode,
                                    Message = "SomeOtherString"
                                },
                                new Error {
                                    Code = expectedDetailsErrorCode,
                                    Message = "AThirdString"
                                }
                            }
                        };

                        // Act
                        var result = _factoryUnderTest.Create(errorCode, errorTarget, errorMessage);

                        // Assert
                        expectedError.AssertEqual(result);
                    }
                }

                public class WhenErrorMessageIsAnotherType : Create
                {
                    [Fact]
                    public void Should_call_ToString_to_create_the_Message()
                    {
                        // Arrange
                        var errorCode = "SomeErrorCode";
                        var errorTarget = "SomeErrorTarget";
                        var errorMessage = new { };
                        var expectedErrorMessage = errorMessage.ToString();
                        var expectedError = new Error
                        {
                            Code = errorCode,
                            Target = errorTarget,
                            Message = expectedErrorMessage
                        };

                        // Act
                        var result = _factoryUnderTest.Create(errorCode, errorTarget, errorMessage);

                        // Assert
                        expectedError.AssertEqual(result);
                    }
                }
            }
        }
    }
}
