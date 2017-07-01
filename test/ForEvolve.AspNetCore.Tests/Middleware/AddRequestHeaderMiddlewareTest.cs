using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Xunit;
using Moq;
using System.Threading.Tasks;
using ForEvolve.AspNetCore.Middleware.Helpers;

namespace ForEvolve.AspNetCore
{
    public class AddRequestHeaderMiddlewareTest
    {
        public class Invoke
        {
            private readonly Mock<HttpContext> _contextMock = new Mock<HttpContext>();
            private readonly Mock<HttpRequest> _requestMock = new Mock<HttpRequest>();
            private readonly Mock<IHeaderDictionary> _headersMock = new Mock<IHeaderDictionary>();

            public Invoke()
            {
                // Arrange
                _contextMock
                    .Setup(x => x.Request)
                    .Returns(_requestMock.Object);
                _requestMock
                    .Setup(x => x.Headers)
                    .Returns(_headersMock.Object);
                _headersMock
                    .Setup(x => x.Add(AddRequestHeaderMiddlewareFake.FakeHeaderName, AddRequestHeaderMiddlewareFake.FakeValue))
                    .Verifiable();
            }

            [Fact]
            public void Should_add_the_expected_http_header_to_the_request()
            {
                // Arrange
                var middleware = new AddRequestHeaderMiddlewareFake(null);

                // Act
                var result = middleware.Invoke(_contextMock.Object);

                // Assert
                _headersMock.Verify(
                    x => x.Add(AddRequestHeaderMiddlewareFake.FakeHeaderName, AddRequestHeaderMiddlewareFake.FakeValue),
                    Times.Once,
                    failMessage: $"Should add the header {AddRequestHeaderMiddlewareFake.FakeHeaderName} with value {AddRequestHeaderMiddlewareFake.FakeValue}."
                );
            }

            [Fact]
            public void Should_not_add_a_http_header_to_the_request_when_ProcessRequest_return_false()
            {
                // Arrange
                var middleware = new AddRequestHeaderMiddlewareFake(
                    null, 
                    processRequest: false
                );

                // Act
                var result = middleware.Invoke(_contextMock.Object);

                // Assert
                _headersMock.Verify(
                    x => x.Add(AddRequestHeaderMiddlewareFake.FakeHeaderName, AddRequestHeaderMiddlewareFake.FakeValue), 
                    Times.Never,
                    failMessage: $"Should not add the header {AddRequestHeaderMiddlewareFake.FakeHeaderName} with value {AddRequestHeaderMiddlewareFake.FakeValue}."
                );
            }
        }
    }
}
