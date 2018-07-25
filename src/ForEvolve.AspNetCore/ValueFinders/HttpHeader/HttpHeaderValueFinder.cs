using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ForEvolve.AspNetCore
{
    public class HttpHeaderValueFinder : IHttpHeaderValueFinder
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpHeaderValueFinder(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string FindHeader(string key)
        {
            var header = _httpContextAccessor
                .HttpContext
                .Request
                .Headers[key]
                .FirstOrDefault();
            if (header == null)
            {
                throw new HttpHeaderValueFinderException(key);
            }
            return header;
        }
    }
}