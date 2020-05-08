using Microsoft.AspNetCore.Hosting;

using System;

namespace ForEvolve.XUnit.HttpTests
{
    public interface IHttpTestServerBuilder
    {
        IHttpTestServer Create(Func<IWebHostBuilder> webHostBuilderImplementationFactory);
    }

    public interface IHttpTestServerBuilder<TStartup> : IHttpTestServerBuilder
         where TStartup : class
    {
        IHttpTestServer Create();
        IHttpTestServer Create(Action<IWebHostBuilder> webHostBuilderSetup);
    }
}
