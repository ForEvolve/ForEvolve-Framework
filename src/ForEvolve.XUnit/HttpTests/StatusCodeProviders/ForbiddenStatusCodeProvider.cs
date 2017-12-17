using Microsoft.AspNetCore.Http;

namespace ForEvolve.XUnit.HttpTests
{
    public sealed class ForbiddenStatusCodeProvider : StatusCodeProvider
    {
        public const int ExpectedStatusCode = StatusCodes.Status403Forbidden;

        public ForbiddenStatusCodeProvider() : base(ExpectedStatusCode) { }
    }
}
