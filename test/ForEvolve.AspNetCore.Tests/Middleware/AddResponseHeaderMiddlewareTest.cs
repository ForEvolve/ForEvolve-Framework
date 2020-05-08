using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Xunit;
using Moq;
using System.Threading.Tasks;
using ForEvolve.AspNetCore.Middleware.Helpers;
using Microsoft.Extensions.Primitives;

namespace ForEvolve.AspNetCore
{
    public class AddResponseHeaderMiddlewareTest
    {
        public class InvokeAsync
        {
            private readonly Mock<HttpContext> _contextMock = new Mock<HttpContext>();
            private readonly Mock<HttpResponse> _responseMock = new Mock<HttpResponse>();
            private readonly Mock<IHeaderDictionary> _headersMock = new Mock<IHeaderDictionary>();

            public InvokeAsync()
            {
                // Arrange
                _contextMock
                    .Setup(x => x.Response)
                    .Returns(_responseMock.Object);
                _responseMock
                    .Setup(x => x.Headers)
                    .Returns(_headersMock.Object);
                _headersMock
                    .Setup(x => x.Add(It.IsAny<string>(), It.IsAny<StringValues>()))
                    .Verifiable();
            }

            [Fact]
            public void Should_add_the_expected_http_header_to_the_request()
            {
                // Arrange
                var middleware = new AddResponseHeaderMiddlewareFake(null);

                // Act
                var result = middleware.InvokeAsync(_contextMock.Object);

                // Assert
                _headersMock.Verify(
                    x => x.Add(AddResponseHeaderMiddlewareFake.FakeHeaderName, AddResponseHeaderMiddlewareFake.FakeValue),
                    Times.Once,
                    failMessage: $"Should add the header {AddResponseHeaderMiddlewareFake.FakeHeaderName} with value {AddResponseHeaderMiddlewareFake.FakeValue}."
                );
            }

            [Fact]
            public void Should_not_add_a_http_header_to_the_request_when_ProcessRequest_return_false()
            {
                // Arrange
                var middleware = new AddResponseHeaderMiddlewareFake(
                    null, 
                    processRequest: false
                );

                // Act
                var result = middleware.InvokeAsync(_contextMock.Object);

                // Assert
                _headersMock.Verify(
                    x => x.Add(AddResponseHeaderMiddlewareFake.FakeHeaderName, AddResponseHeaderMiddlewareFake.FakeValue), 
                    Times.Never,
                    failMessage: $"Should not add the header {AddResponseHeaderMiddlewareFake.FakeHeaderName} with value {AddResponseHeaderMiddlewareFake.FakeValue}."
                );
            }
        }
    }
}
