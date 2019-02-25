using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ForEvolve.XUnit.Http
{
    public class HttpResponseFake : HttpResponse
    {
        public HttpResponseFake(HttpContext httpContext, IHeaderDictionary headers, IResponseCookies cookies)
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
