using Microsoft.AspNetCore.Http;

namespace ForEvolve.XUnit.HttpTests
{
    public sealed class NotFoundStatusCodeProvider : StatusCodeProvider
    {
        public NotFoundStatusCodeProvider() : base(StatusCodes.Status404NotFound) { }
    }
}
