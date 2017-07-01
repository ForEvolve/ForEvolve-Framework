using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace ForEvolve.AspNetCore
{
    public class AddRequestIdMiddleware : AddRequestHeaderMiddleware
    {
        public const string DefaultHeaderName = "X-ForEvolve-RequestId";

        public AddRequestIdMiddleware(RequestDelegate next, string headerName = DefaultHeaderName)
            : base(next, headerName)
        {
        }

        protected override string GenerateHeaderValue(HttpContext context)
        {
            return $"{DateTime.Now.ToFileTimeUtc()}|{context.TraceIdentifier}";
        }
    }
}
