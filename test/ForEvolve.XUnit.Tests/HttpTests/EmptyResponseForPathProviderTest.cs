using ForEvolve.XUnit.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.XUnit.HttpTests
{
    public class EmptyResponseForPathProviderTest
    {
        private readonly EmptyResponseForPathProvider _providerUnderTest;

        private readonly string _expectedPath;
        private readonly string _expectedMethod;

        private readonly HttpContextHelper _httpContextHelper;

        public EmptyResponseForPathProviderTest()
        {
            _httpContextHelper = new HttpContextHelper();

            _expectedMethod = "POST";
            _expectedPath = "/whatever";

            _providerUnderTest = new EmptyResponseForPathProvider(
                _expectedMethod,
                _expectedPath
            );
        }
        public class ResponseText : EmptyResponseForPathProviderTest
        {
            [Fact]
            public void Should_throw_WrongEndpointException_when_Method_is_incorrect()
            {
                // Arrange
                _httpContextHelper.HttpRequest.Method = "GET";
                _httpContextHelper.HttpRequest.Path = _expectedPath;

                // Act & Assert
                var exception = Assert.Throws<WrongEndpointException>(
                    () => _providerUnderTest.ResponseText(_httpContextHelper.HttpContextMock.Object)
                );
                Assert.Equal(_expectedMethod, exception.ExpectedMethod);
                Assert.Equal(_expectedPath, exception.ExpectedPath);
                Assert.Equal("GET", exception.ActualMethod);
                Assert.Equal(_expectedPath, exception.ActualPath);
            }

            [Fact]
            public void Should_throw_WrongEndpointException_when_Path_is_incorrect()
            {
                // Arrange
                _httpContextHelper.HttpRequest.Method = _expectedMethod;
                _httpContextHelper.HttpRequest.Path = "/some/other/path";

                // Act & Assert
                var exception = Assert.Throws<WrongEndpointException>(
                    () => _providerUnderTest.ResponseText(_httpContextHelper.HttpContextMock.Object)
                );
                Assert.Equal(_expectedMethod, exception.ExpectedMethod);
                Assert.Equal(_expectedPath, exception.ExpectedPath);
                Assert.Equal(_expectedMethod, exception.ActualMethod);
                Assert.Equal("/some/other/path", exception.ActualPath);
            }

            [Fact]
            public void Should_return_an_empty_response_when_Path_and_Method_are_correct()
            {
                // Arrange
                _httpContextHelper.HttpRequest.Method = _expectedMethod;
                _httpContextHelper.HttpRequest.Path = _expectedPath;

                // Act
                var result = _providerUnderTest.ResponseText(_httpContextHelper.HttpContextMock.Object);

                // Assert
                Assert.Empty(result);
            }
        }
    }
}
