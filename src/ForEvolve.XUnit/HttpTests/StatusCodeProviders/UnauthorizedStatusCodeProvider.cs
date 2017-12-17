using Microsoft.AspNetCore.Http;

namespace ForEvolve.XUnit.HttpTests
{
    public sealed class UnauthorizedStatusCodeProvider : StatusCodeProvider
    {
        public const int ExpectedStatusCode = StatusCodes.Status401Unauthorized;

        public UnauthorizedStatusCodeProvider() : base(ExpectedStatusCode) { }
    }
}
