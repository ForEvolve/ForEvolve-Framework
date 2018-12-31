using Microsoft.AspNetCore.Http;
using System.IO;
using Xunit;

namespace ForEvolve.XUnit.Http
{
    public static class HttpResponseExtensions
    {
        public static void BodyEqual(this HttpResponse httpResponse, string expectedBody)
        {
            httpResponse.Body.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(httpResponse.Body))
            {
                var result = reader.ReadToEnd();
                Assert.Equal(expectedBody, result);
            }
        }
    }
}
