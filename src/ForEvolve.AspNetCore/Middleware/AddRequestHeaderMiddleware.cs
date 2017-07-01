using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.AspNetCore
{
    public abstract class AddRequestHeaderMiddleware : BaseMiddleware
    {
        protected string HeaderName { get; }

        public AddRequestHeaderMiddleware(RequestDelegate next, string headerName)
            : base (next)
        {
            if (string.IsNullOrWhiteSpace(headerName)) { throw new ArgumentNullException(nameof(headerName)); }
            HeaderName = headerName;
        }

        protected override Task InternalInvokeAsync(HttpContext context)
        {
            if (ProcessRequest(context))
            {
                var value = GenerateHeaderValue(context);
                context.Request.Headers.Add(HeaderName, value);
            }
            return Task.FromResult(0);
        }

        protected abstract string GenerateHeaderValue(HttpContext context);

        protected virtual bool ProcessRequest(HttpContext context)
        {
            return true;
        }
    }
}
