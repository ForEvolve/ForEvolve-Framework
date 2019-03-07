using Microsoft.AspNetCore.Http;

namespace ForEvolve.XUnit.HttpTests
{
    public sealed class ForbiddenStatusCodeProvider : StatusCodeProvider
    {
        public ForbiddenStatusCodeProvider() : base(StatusCodes.Status403Forbidden) { }
    }
}
