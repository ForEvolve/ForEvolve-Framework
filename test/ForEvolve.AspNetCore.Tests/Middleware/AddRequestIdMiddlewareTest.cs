using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ForEvolve.AspNetCore
{
    public class AddRequestIdMiddlewareTest
    {
        public class GenerateHeaderValue
        {
            [Fact]
            public void Should_return_an_id_that_combine_request_time_and_TraceIdentifier()
            {
                // Arrange
                var expectedTimeLength = DateTime.Now.ToFileTimeUtc().ToString().Length;
                var expectedTraceIdentifier = "AddRequestIdMiddlewareTest-TraceIdentifier";
                var httpContextMock = new Mock<HttpContext>();
                httpContextMock.Setup(x => x.TraceIdentifier)
                    .Returns(expectedTraceIdentifier);
                var middleware = new AddRequestIdMiddlewareProtectedAccess();

                // Act
                var result = middleware.AccessGenerateHeaderValue(httpContextMock.Object);

                // Assert
                var resultParts = result.Split('|');
                Assert.Equal(expectedTimeLength, resultParts[0].Length);
                Assert.Equal(expectedTraceIdentifier, resultParts[1]);
            }
        }

        private class AddRequestIdMiddlewareProtectedAccess : AddRequestIdMiddleware
        {
            public AddRequestIdMiddlewareProtectedAccess()
                : base(null, DefaultHeaderName)
            {
            }

            public string AccessGenerateHeaderValue(HttpContext context)
            {
                return GenerateHeaderValue(context);
            }
        }
    }
}
