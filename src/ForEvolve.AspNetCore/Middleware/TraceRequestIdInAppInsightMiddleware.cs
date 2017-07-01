using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ForEvolve.AspNetCore
{
    public class TraceRequestIdInAppInsightMiddleware : BaseMiddleware
    {
        public TraceRequestIdInAppInsightMiddleware(RequestDelegate next)
            : base(next)
        {

        }

        protected override Task InternalInvokeAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
