using Microsoft.AspNetCore.Http;

namespace ForEvolve.XUnit.HttpTests
{
    public sealed class OkStatusCodeProvider : StatusCodeProvider
    {
        public const int ExpectedStatusCode = StatusCodes.Status200OK;

        public OkStatusCodeProvider() : base(ExpectedStatusCode) { }
    }
}
