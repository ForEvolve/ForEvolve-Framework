using Microsoft.AspNetCore.Http;

namespace ForEvolve.XUnit.HttpTests
{
    public sealed class NotFoundStatusCodeProvider : StatusCodeProvider
    {
        public const int ExpectedStatusCode = StatusCodes.Status404NotFound;

        public NotFoundStatusCodeProvider() : base(ExpectedStatusCode) { }
    }
}
