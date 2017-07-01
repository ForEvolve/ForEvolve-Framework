using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Xunit;
using Moq;
using System.Threading.Tasks;

namespace ForEvolve.AspNetCore.Middleware.Helpers
{
    internal class AddRequestHeaderMiddlewareFake : AddRequestHeaderMiddleware
    {
        public const string FakeHeaderName = "FakeHeaderName";
        public const string FakeValue = "FakeValue";
        public bool ProcessRequestValue { get; }

        public AddRequestHeaderMiddlewareFake(RequestDelegate next, bool processRequest = true)
            : base(next, FakeHeaderName)
        {
            ProcessRequestValue = processRequest;
        }

        protected override string GenerateHeaderValue(HttpContext context)
        {
            return FakeValue;
        }

        protected override bool ProcessRequest(HttpContext context)
        {
            return ProcessRequestValue;
        }
    }
}