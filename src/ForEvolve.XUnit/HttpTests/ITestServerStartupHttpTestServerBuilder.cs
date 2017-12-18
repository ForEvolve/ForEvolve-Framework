namespace ForEvolve.XUnit.HttpTests
{
    public interface ITestServerStartupHttpTestServerBuilder : IHttpTestServerBuilder<TestServerStartup>
    {
        IHttpTestServer Create<TStatusCodeProvider>()
            where TStatusCodeProvider : class, IStatusCodeProvider;

        IHttpTestServer Create<TStatusCodeProvider, TResponseProvider>()
            where TStatusCodeProvider : class, IStatusCodeProvider
            where TResponseProvider : class, IResponseProvider;

        IHttpTestServer Create(IStatusCodeProvider statusCodeProvider);
        IHttpTestServer Create(IStatusCodeProvider statusCodeProvider, IResponseProvider responseProvider);

        IHttpTestServer Create<TStatusCodeProvider>(IResponseProvider responseProvider)
            where TStatusCodeProvider : class, IStatusCodeProvider;
        IHttpTestServer Create<TResponseProvider>(IStatusCodeProvider statusCodeProvider)
            where TResponseProvider : class, IResponseProvider;
    }
}
