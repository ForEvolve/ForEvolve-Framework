using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.AspNetCore.Middleware
{
    public abstract class AddResponseHeaderMiddleware : BaseMiddleware
    {
        protected string HeaderName { get; }

        public AddResponseHeaderMiddleware(RequestDelegate next, string headerName)
            : base(next)
        {
            if (string.IsNullOrWhiteSpace(headerName)) { throw new ArgumentNullException(nameof(headerName)); }
            HeaderName = headerName;
        }

        protected override Task InternalInvokeAsync(HttpContext context)
        {
            if (ProcessRequest(context))
            {
                var value = GenerateHeaderValue(context);
                context.Response.Headers.Add(HeaderName, value);
            }
            return Task.CompletedTask;
        }

        protected abstract string GenerateHeaderValue(HttpContext context);

        protected virtual bool ProcessRequest(HttpContext context)
        {
            return true;
        }
    }
}
