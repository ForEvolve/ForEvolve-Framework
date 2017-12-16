using ForEvolve.XUnit.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.XUnit.HttpTests
{
    public class JsonPathResponseProviderTest
    {
        private readonly JsonPathResponseProvider _providerUnderTest;

        private readonly string _expectedMethod;
        private readonly string _expectedPath;
        private readonly JsonPathResponseObject _successObject;
        private readonly JsonPathResponseObject _failureObject;

        private readonly string _expectedSuccessResponseText;
        private readonly string _expectedFailureResponseText;

        private readonly HttpContextHelper _httpContextHelper;

        public JsonPathResponseProviderTest()
        {
            _httpContextHelper = new HttpContextHelper();

            _expectedMethod = "POST";
            _expectedPath = "/whatever";
            _successObject = new JsonPathResponseObject { IsSuccess = true };
            _failureObject = new JsonPathResponseObject { IsSuccess = false };

            _expectedSuccessResponseText = JsonConvert.SerializeObject(_successObject);
            _expectedFailureResponseText = JsonConvert.SerializeObject(_failureObject);

            _providerUnderTest = new JsonPathResponseProvider(
                _expectedMethod,
                _expectedPath,
                _successObject,
                _failureObject
            );
        }

        public class JsonPathResponseObject
        {
            public bool IsSuccess { get; set; }
        }

        public class ResponseText : JsonPathResponseProviderTest
        {
            [Fact]
            public void Should_return_failure_if_Method_is_incorrect()
            {
                // Arrange
                _httpContextHelper.HttpRequest.Method = "GET";
                _httpContextHelper.HttpRequest.Path = _expectedPath;

                // Act
                var result = _providerUnderTest.ResponseText(_httpContextHelper.HttpContextMock.Object);

                // Assert
                Assert.Equal(_expectedFailureResponseText, result);
            }

            [Fact]
            public void Should_return_failure_if_Path_is_incorrect()
            {
                // Arrange
                _httpContextHelper.HttpRequest.Method = _expectedMethod;
                _httpContextHelper.HttpRequest.Path = "/some/other/path";

                // Act
                var result = _providerUnderTest.ResponseText(_httpContextHelper.HttpContextMock.Object);

                // Assert
                Assert.Equal(_expectedFailureResponseText, result);
            }

            [Fact]
            public void Should_return_success_if_Path_and_Method_are_correct()
            {
                // Arrange
                _httpContextHelper.HttpRequest.Method = _expectedMethod;
                _httpContextHelper.HttpRequest.Path = _expectedPath;

                // Act
                var result = _providerUnderTest.ResponseText(_httpContextHelper.HttpContextMock.Object);

                // Assert
                Assert.Equal(_expectedSuccessResponseText, result);
            }
        }
    }
}
