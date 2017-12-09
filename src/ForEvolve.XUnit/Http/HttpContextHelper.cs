using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ForEvolve.XUnit.Http
{
    public class HttpContextHelper
    {
        public Mock<IHttpContextAccessor> Mock { get; }
        public Mock<HttpContext> HttpContextMock { get; }
        public HttpRequest HttpRequest { get; }
        private HeaderDictionary HeaderDictionary { get; }

        public HttpContextHelper()
        {
            // Create options
            Options = new HttpContextHelperOptions();

            // Create mocks
            Mock = new Mock<IHttpContextAccessor>();
            HttpContextMock = new Mock<HttpContext>();
            HeaderDictionary = new HeaderDictionary();
            HttpRequest = new HttpRequestFake(HttpContextMock.Object, HeaderDictionary);

            Mock
                .Setup(x => x.HttpContext)
                .Returns(HttpContextMock.Object);
            HttpContextMock
                .Setup(x => x.Request)
                .Returns(() => HttpRequest);

            //HeaderDictionaryMock
            //    .Setup(x => x["Authorization"])
            //    .Returns(() => Options.ExpectedAuthorizationHeader);
        }

        public HttpContextHelperOptions Options { get; set; }
    }

    public class HttpContextHelperOptions
    {

        //public string AuthorizationBearerToken { get; set; }
        //public string ExpectedAuthorizationHeader
        //{
        //    get
        //    {
        //        return $"Bearer {AuthorizationBearerToken}";
        //    }
        //}
    }

    public class HttpRequestFake : HttpRequest
    {
        public HttpRequestFake(HttpContext httpContext, IHeaderDictionary headers)
        {
            HttpContext = httpContext;
            Headers = headers;
        }

        public override HttpContext HttpContext { get; }

        public override string Method { get; set; }
        public override string Scheme { get; set; }
        public override bool IsHttps { get; set; }
        public override HostString Host { get; set; }
        public override PathString PathBase { get; set; }
        public override PathString Path { get; set; }
        public override QueryString QueryString { get; set; }
        public override IQueryCollection Query { get; set; }
        public override string Protocol { get; set; }

        public override IHeaderDictionary Headers { get; }

        public override IRequestCookieCollection Cookies { get; set; }
        public override long? ContentLength { get; set; }
        public override string ContentType { get; set; }
        public override Stream Body { get; set; }

        public override bool HasFormContentType => throw new NotImplementedException();

        public override IFormCollection Form { get; set; }

        public override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
