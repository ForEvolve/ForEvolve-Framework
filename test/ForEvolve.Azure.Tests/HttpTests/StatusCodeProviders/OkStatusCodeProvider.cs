using Microsoft.AspNetCore.Http;

namespace ForEvolve.XUnit.HttpTests
{
    public sealed class OkStatusCodeProvider : StatusCodeProvider
    {
        public OkStatusCodeProvider() : base(StatusCodes.Status200OK) { }
    }
}
