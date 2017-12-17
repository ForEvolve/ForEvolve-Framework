using Microsoft.AspNetCore.Http;

namespace ForEvolve.XUnit.HttpTests
{
    public sealed class InternalServerErrorStatusCodeProvider : StatusCodeProvider
    {
        public const int ExpectedStatusCode = StatusCodes.Status500InternalServerError;

        public InternalServerErrorStatusCodeProvider() : base(ExpectedStatusCode) { }
    }
}
