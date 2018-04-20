using Microsoft.AspNetCore.TestHost;

using System;
using System.Net.Http;

namespace ForEvolve.XUnit.HttpTests
{
    public interface IHttpTestServer : IDisposable
    {
        TestServer Server { get; }
        HttpClient Client { get; }
    }
}
