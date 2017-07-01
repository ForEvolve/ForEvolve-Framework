using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.ApplicationInsights
{
    public class TrackExceptionsFilterAttributeTest
    {
        protected TrackExceptionsFilterAttribute AttributeUnderTest { get; }
        protected Mock<ITelemetryClient> _telemetryClientMock;

        public TrackExceptionsFilterAttributeTest()
        {
            _telemetryClientMock = new Mock<ITelemetryClient>();
            AttributeUnderTest = new TrackExceptionsFilterAttribute(_telemetryClientMock.Object);
        }

        public class OnException : TrackExceptionsFilterAttributeTest
        {
            [Fact]
            public void Should_TrackException()
            {
                // Arrange
                var expectedException = new Exception();
                var context = new ExceptionContext(CreateActionContext(), new List<IFilterMetadata>())
                {
                    Exception = expectedException
                };
                _telemetryClientMock
                    .Setup(x => x.TrackException(expectedException, null, null))
                    .Verifiable();

                // Act
                AttributeUnderTest.OnException(context);

                // Assert
                _telemetryClientMock
                    .Verify(x => x.TrackException(expectedException, null, null), Times.Once);
            }

            [Fact]
            public void Should_not_Track_null_Exception()
            {
                // Arrange
                var context = new ExceptionContext(CreateActionContext(), new List<IFilterMetadata>())
                {
                    Exception = default(Exception)
                };
                _telemetryClientMock
                    .Setup(x => x.TrackException(It.IsAny<Exception>(), null, null))
                    .Verifiable();

                // Act
                AttributeUnderTest.OnException(context);

                // Assert
                _telemetryClientMock
                    .Verify(x => x.TrackException(It.IsAny<Exception>(), null, null), Times.Never);
            }

            [Fact]
            public void Should_not_TrackException_on_null_context()
            {
                // Arrange
                _telemetryClientMock
                    .Setup(x => x.TrackException(It.IsAny<Exception>(), null, null))
                    .Verifiable();

                // Act
                AttributeUnderTest.OnException(null);

                // Assert
                _telemetryClientMock
                    .Verify(x => x.TrackException(It.IsAny<Exception>(), null, null), Times.Never);
            }
        }

        public class OnExceptionAsync : TrackExceptionsFilterAttributeTest
        {
            [Fact]
            public async Task Should_TrackException()
            {
                // Arrange
                var expectedException = new Exception();
                var context = new ExceptionContext(CreateActionContext(), new List<IFilterMetadata>())
                {
                    Exception = expectedException
                };
                _telemetryClientMock
                    .Setup(x => x.TrackException(expectedException, null, null))
                    .Verifiable();

                // Act
                await AttributeUnderTest.OnExceptionAsync(context);

                // Assert
                _telemetryClientMock
                    .Verify(x => x.TrackException(expectedException, null, null), Times.Once);
            }

            [Fact]
            public async Task Should_not_Track_null_Exception()
            {
                // Arrange
                var context = new ExceptionContext(CreateActionContext(), new List<IFilterMetadata>())
                {
                    Exception = default(Exception)
                };
                _telemetryClientMock
                    .Setup(x => x.TrackException(It.IsAny<Exception>(), null, null))
                    .Verifiable();

                // Act
                await AttributeUnderTest.OnExceptionAsync(context);

                // Assert
                _telemetryClientMock
                    .Verify(x => x.TrackException(It.IsAny<Exception>(), null, null), Times.Never);
            }
        }

        protected ActionContext CreateActionContext()
        {
            var httpContext = new DefaultHttpContext();
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
            return actionContext;
        }
    }
}
