using ForEvolve.Testing.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.AspNetCore.Mvc
{
    public class UrlHelperExtensionsTest
    {
        public class ToFullyQualifiedUri
        {
            [Fact]
            public void Should_return_a_fully_qualified_uri()
            {
                // Arrange
                var inputUri = "/toto.html";
                var expectedUri = "http://a.com/toto.html";
                var httpHelper = new MvcContextHelper();
                httpHelper.HttpContextHelper.HttpRequest.Host = new HostString("a.com");
                httpHelper.HttpContextHelper.HttpRequest.Scheme = "http";

                // Act
                var result = httpHelper.UrlHelperMock.Object.ToFullyQualifiedUri(inputUri);

                // Assert
                Assert.Equal(expectedUri, result);
            }

            [Fact]
            public void Should_return_a_fully_qualified_uri_with_port_number()
            {
                // Arrange
                var inputUri = "/toto.html";
                var expectedUri = "http://a.com:1234/toto.html";
                var httpHelper = new MvcContextHelper();
                httpHelper.HttpContextHelper.HttpRequest.Host = new HostString("a.com", 1234);
                httpHelper.HttpContextHelper.HttpRequest.Scheme = "http";

                // Act
                var result = httpHelper.UrlHelperMock.Object.ToFullyQualifiedUri(inputUri);

                // Assert
                Assert.Equal(expectedUri, result);
            }

            [Fact]
            public void Should_guard_against_null_uri()
            {
                // Arrange
                var httpHelper = new MvcContextHelper();

                // Act & Assert
                Assert.Throws<ArgumentNullException>(
                    "uri", 
                    () => httpHelper.UrlHelperMock.Object.ToFullyQualifiedUri(null)
                );
            }

            [Fact]
            public void Should_throw_ArgumentException_when_uri_does_not_start_with_a_slash()
            {
                // Arrange
                var httpHelper = new MvcContextHelper();
                var uri = "no/beginning/slash.html";

                // Act & Assert
                Assert.Throws<ArgumentException>(
                    "uri",
                    () => httpHelper.UrlHelperMock.Object.ToFullyQualifiedUri(uri)
                );
            }
        }
    }
}
