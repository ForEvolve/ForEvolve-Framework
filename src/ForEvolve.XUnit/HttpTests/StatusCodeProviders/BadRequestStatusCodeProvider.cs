using Microsoft.AspNetCore.Http;

namespace ForEvolve.XUnit.HttpTests
{
    public sealed class BadRequestStatusCodeProvider : StatusCodeProvider
    {
        public const int ExpectedStatusCode = StatusCodes.Status400BadRequest;
        public BadRequestStatusCodeProvider() : base(ExpectedStatusCode) { }
    }
}
