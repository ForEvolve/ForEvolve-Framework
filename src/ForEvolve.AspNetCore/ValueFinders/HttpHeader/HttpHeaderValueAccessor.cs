using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ForEvolve.AspNetCore
{
    public class HttpHeaderValueAccessor : IHttpHeaderValueAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpHeaderValueAccessor(IHttpContextAccessor httpContextAccessor)
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
                throw new HttpHeaderValueAccessorException(key);
            }
            return header;
        }
    }
}