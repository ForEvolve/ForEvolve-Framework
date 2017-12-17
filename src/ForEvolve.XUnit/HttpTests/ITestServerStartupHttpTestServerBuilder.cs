namespace ForEvolve.XUnit.HttpTests
{
    public interface ITestServerStartupHttpTestServerBuilder : IHttpTestServerBuilder<TestServerStartup>
    {
        IHttpTestServer Arrange<TStatusCodeProvider>()
            where TStatusCodeProvider : class, IStatusCodeProvider;

        IHttpTestServer Arrange<TStatusCodeProvider, TResponseProvider>()
            where TStatusCodeProvider : class, IStatusCodeProvider
            where TResponseProvider : class, IResponseProvider;

        IHttpTestServer Arrange(IStatusCodeProvider statusCodeProvider);
        IHttpTestServer Arrange(IResponseProvider responseProvider);
        IHttpTestServer Arrange(IStatusCodeProvider statusCodeProvider, IResponseProvider responseProvider);

        IHttpTestServer Arrange<TStatusCodeProvider>(IResponseProvider responseProvider)
            where TStatusCodeProvider : class, IStatusCodeProvider;
        IHttpTestServer Arrange<TResponseProvider>(IStatusCodeProvider statusCodeProvider)
            where TResponseProvider : class, IResponseProvider;
    }
}
