using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace ForEvolve.XUnit.HttpTests
{
    public class JsonPathResponseProvider : IResponseProvider
    {
        private readonly object _successObject;
        private readonly string _expectedPath;
        private readonly string _expectedMethod;

        public JsonPathResponseProvider(string expectedMethod, string expectedPath, object successObject)
        {
            _expectedPath = expectedPath ?? throw new ArgumentNullException(nameof(expectedPath));
            _expectedMethod = expectedMethod ?? throw new ArgumentNullException(nameof(expectedMethod));
            _successObject = successObject ?? throw new ArgumentNullException(nameof(successObject));
        }

        public string ResponseText(HttpContext context)
        {
            var samePath = context.Request.Path.Value == _expectedPath;
            var sameMethod = context.Request.Method == _expectedMethod;
            if (samePath && sameMethod)
            {
                return JsonConvert.SerializeObject(_successObject);
            }
            throw new WrongEndpointException(
                _expectedMethod, _expectedPath,
                context.Request.Method, context.Request.Path.Value
            );
        }
    }
}
