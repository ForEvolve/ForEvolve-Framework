using Microsoft.AspNetCore.Http;

namespace ForEvolve.XUnit.HttpTests
{
    public sealed class InternalServerErrorStatusCodeProvider : StatusCodeProvider
    {
        public InternalServerErrorStatusCodeProvider() : base(StatusCodes.Status500InternalServerError) { }
    }
}
