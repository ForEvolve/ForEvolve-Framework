using Microsoft.AspNetCore.Http;
using System.IO;
using Xunit;

namespace ForEvolve.XUnit.Http
{
    public static class HttpResponseExtensions
    {
        public static void BodyShouldEqual(this HttpResponse httpResponse, string expectedBody)
        {
            httpResponse.Body.ShouldEqual(expectedBody);
        }

        public static void ShouldEqual(this Stream stream, string expectedResult)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(stream))
            {
                var result = reader.ReadToEnd();
                Assert.Equal(expectedResult, result);
            }
        }
    }
}
