using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.XUnit.HttpTests
{
    public class HttpTestServerBuilderTest
    {
        [Fact]
        public async Task ManualHostBuilding()
        {
            // This should be a class member and could be used in multiple tests
            IHttpTestServerBuilder _httpTestServerBuilder = new HttpTestServerBuilder();

            // Arrange
            using (var testServer = _httpTestServerBuilder.Arrange(() => WebHost.CreateDefaultBuilder().Configure(appBuilder =>
            {
                appBuilder.Run(context =>
                {
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    return Task.CompletedTask;
                });
            })))
            {
                // Act
                var result = await testServer.Client.GetAsync("/whatever");

                // Assert
                Assert.Equal(StatusCodes.Status200OK, (int)result.StatusCode);
            }
        }
    }
}
