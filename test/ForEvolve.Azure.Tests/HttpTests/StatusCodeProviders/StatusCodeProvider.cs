namespace ForEvolve.XUnit.HttpTests
{
    public class StatusCodeProvider : IStatusCodeProvider
    {
        public StatusCodeProvider(int statusCode)
        {
            StatusCode = statusCode;
        }
        public int StatusCode { get; }
    }
}
