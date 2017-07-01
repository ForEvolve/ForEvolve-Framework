using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ForEvolve.AspNetCore.Middleware.Helpers
{
    internal class BaseMiddlewareFake : BaseMiddleware
    {
        public BaseMiddlewareFake(RequestDelegate next)
            : base(next)
        {
        }

        protected override Task InternalInvokeAsync(HttpContext context)
        {
            return Task.FromResult(0);
        }
    }
}
