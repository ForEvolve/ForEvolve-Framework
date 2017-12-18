using Microsoft.AspNetCore.Http;

namespace ForEvolve.XUnit.HttpTests
{
    public sealed class NoContentStatusCodeProvider : StatusCodeProvider
    {
        public const int ExpectedStatusCode = StatusCodes.Status204NoContent;

        public NoContentStatusCodeProvider() : base(ExpectedStatusCode) { }
    }

}
