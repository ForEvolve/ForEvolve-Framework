using Microsoft.AspNetCore.Http;

namespace ForEvolve.AspNetCore.Middleware.Helpers
{
    internal class AddResponseHeaderMiddlewareFake : AddResponseHeaderMiddleware
    {
        public const string FakeHeaderName = "FakeHeaderName";
        public const string FakeValue = "FakeValue";
        public bool ProcessRequestValue { get; }

        public AddResponseHeaderMiddlewareFake(RequestDelegate next, bool processRequest = true)
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