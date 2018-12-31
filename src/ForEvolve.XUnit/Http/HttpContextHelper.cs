using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.XUnit.Http
{
    public class HttpContextHelper
    {
        public Mock<IHttpContextAccessor> Mock { get; }
        public Mock<HttpContext> HttpContextMock { get; }
        public HttpRequest HttpRequest { get; }
        private HeaderDictionary HeaderDictionary { get; }
        public Mock<IResponseCookies> ResponseCookiesMock { get; set; }
        public HttpResponse HttpResponse { get; }

        public HttpContextHelper()
        {
            // Create options
            Options = new HttpContextHelperOptions();

            // Create mocks
            Mock = new Mock<IHttpContextAccessor>();
            HttpContextMock = new Mock<HttpContext>();
            HeaderDictionary = new HeaderDictionary();
            HttpRequest = new HttpRequestFake(HttpContextMock.Object, HeaderDictionary);
            ResponseCookiesMock = new Mock<IResponseCookies>();
            HttpResponse = new FakeHttpResponse(
                HttpContextMock.Object, 
                HeaderDictionary, 
                ResponseCookiesMock.Object
            );
            HttpResponse.Body = new MemoryStream();

            Mock
                .Setup(x => x.HttpContext)
                .Returns(HttpContextMock.Object);
            HttpContextMock
                .Setup(x => x.Request)
                .Returns(() => HttpRequest);
            HttpContextMock
                .Setup(x => x.Response)
                .Returns(() => HttpResponse);

            //HeaderDictionaryMock
            //    .Setup(x => x["Authorization"])
            //    .Returns(() => Options.ExpectedAuthorizationHeader);
        }

        public HttpContextHelperOptions Options { get; set; }
    }


    public class FakeHttpResponse : HttpResponse
    {
        public FakeHttpResponse(HttpContext httpContext, IHeaderDictionary headers, IResponseCookies cookies)
        {
            HttpContext = httpContext;
            Headers = headers;
            Cookies = cookies;
        }

        public override HttpContext HttpContext { get; }
        public override IHeaderDictionary Headers { get; }

        public override int StatusCode { get; set; }
        public override Stream Body { get; set; }
        public override long? ContentLength { get; set; }
        public override string ContentType { get; set; }

        public override IResponseCookies Cookies { get; }
        public override bool HasStarted { get; }

        public override void OnCompleted(Func<object, Task> callback, object state)
        {
            throw new NotImplementedException();
        }

        public override void OnStarting(Func<object, Task> callback, object state)
        {
            throw new NotImplementedException();
        }

        public override void Redirect(string location, bool permanent)
        {
            throw new NotImplementedException();
        }
    }
}
