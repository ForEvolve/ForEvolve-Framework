using Microsoft.AspNetCore.Http;

namespace ForEvolve.XUnit.HttpTests
{
    public sealed class BadRequestStatusCodeProvider : StatusCodeProvider
    {
        public BadRequestStatusCodeProvider() : base(StatusCodes.Status400BadRequest) { }
    }
}
