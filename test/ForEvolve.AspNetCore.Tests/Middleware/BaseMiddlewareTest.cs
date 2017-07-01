using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ForEvolve.AspNetCore.Middleware.Helpers;
using Moq;

namespace ForEvolve.AspNetCore
{
    public class BaseMiddlewareTest
    {
        public class Invoke
        {
            private bool _nextHasBeenCalled;
            private HttpContext _nextHasBeenCalledWithHttpContext;

            [Fact]
            public async Task Should_call_the_next_delegate()
            {
                // Arrange
                var contextMock = new Mock<HttpContext>();
                var sut = new BaseMiddlewareFake(RequestDelegate);

                // Act
                await sut.Invoke(contextMock.Object);

                // Assert
                Assert.True(_nextHasBeenCalled);
                Assert.Same(contextMock.Object, _nextHasBeenCalledWithHttpContext);
            }

            private Task RequestDelegate(HttpContext context)
            {
                _nextHasBeenCalled = true;
                _nextHasBeenCalledWithHttpContext = context;
                return Task.FromResult(0);
            }
        }
    }
}
