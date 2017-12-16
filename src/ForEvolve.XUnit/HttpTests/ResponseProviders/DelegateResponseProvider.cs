using Microsoft.AspNetCore.Http;
using System;

namespace ForEvolve.XUnit.HttpTests
{
    public class DelegateResponseProvider : IResponseProvider
    {
        private readonly Func<HttpContext, string> _responseTextDelegate;
        public DelegateResponseProvider(Func<HttpContext, string> responseTextDelegate)
        {
            _responseTextDelegate = responseTextDelegate ?? throw new ArgumentNullException(nameof(responseTextDelegate));
        }

        public string ResponseText(HttpContext context)
        {
            return _responseTextDelegate(context);
        }
    }
}
