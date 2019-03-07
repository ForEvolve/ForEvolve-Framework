using Microsoft.AspNetCore.Http;

namespace ForEvolve.XUnit.HttpTests
{
    public sealed class CreatedStatusCodeProvider : StatusCodeProvider
    {
        public CreatedStatusCodeProvider() : base(StatusCodes.Status201Created) { }
    }
}
