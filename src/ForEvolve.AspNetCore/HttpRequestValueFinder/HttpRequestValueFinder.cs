using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ForEvolve.AspNetCore
{
    public class HttpRequestValueFinder : IHttpRequestValueFinder
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpRequestValueFinder(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string Find(string key)
        {
            var header = FindHeader(key);
            if (header == null)
            {
                throw new HttpRequestValueFinderException(key);
            }
            return header;
        }

        private string FindHeader(string key)
        {
            return _httpContextAccessor
                .HttpContext
                .Request
                .Headers[key]
                .FirstOrDefault();
        }
    }
}