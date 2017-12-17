using Microsoft.AspNetCore.Http;

namespace ForEvolve.XUnit.HttpTests
{
    public sealed class CreatedStatusCodeProvider : StatusCodeProvider
    {
        public const int ExpectedStatusCode = StatusCodes.Status201Created;

        public CreatedStatusCodeProvider() : base(ExpectedStatusCode) { }
    }
}
