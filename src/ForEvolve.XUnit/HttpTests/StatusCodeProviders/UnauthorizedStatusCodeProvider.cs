using Microsoft.AspNetCore.Http;

namespace ForEvolve.XUnit.HttpTests
{
    public sealed class UnauthorizedStatusCodeProvider : StatusCodeProvider
    {
        public UnauthorizedStatusCodeProvider() : base(StatusCodes.Status401Unauthorized) { }
    }
}
