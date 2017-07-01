﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.AspNetCore
{
    public abstract class BaseMiddleware
    {
        private readonly RequestDelegate _next;

        public BaseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await InternalInvokeAsync(context);
            await _next(context);
        }

        protected abstract Task InternalInvokeAsync(HttpContext context);
    }
}
