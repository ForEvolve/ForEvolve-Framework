using Microsoft.AspNetCore.TestHost;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.XUnit.HttpTests
{
    public class HttpTestServer : IHttpTestServer
    {
        public HttpTestServer(TestServer server, HttpClient client)
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public TestServer Server { get; }

        public HttpClient Client { get; }

        #region IDisposable Support

        private bool _isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    Client.Dispose();
                    Server.Dispose();
                }
                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
