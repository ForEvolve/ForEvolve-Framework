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
        private readonly object _failureObject;
        private readonly string _expectedPath;
        private readonly string _expectedMethod;

        public JsonPathResponseProvider(string expectedMethod, string expectedPath, object successObject, object failureObject)
        {
            _expectedPath = expectedPath ?? throw new ArgumentNullException(nameof(expectedPath));
            _expectedMethod = expectedMethod ?? throw new ArgumentNullException(nameof(expectedMethod));
            _successObject = successObject ?? throw new ArgumentNullException(nameof(successObject));
            _failureObject = failureObject;
        }

        public string ResponseText(HttpContext context)
        {
            var samePath = context.Request.Path.Value == _expectedPath;
            var sameMethod = context.Request.Method == _expectedMethod;
            if (samePath && sameMethod)
            {
                return JsonConvert.SerializeObject(_successObject);
            }
#if DEBUG
            else
            {
                var logger = context.RequestServices?.GetService<ILogger<JsonPathResponseProvider>>();
                if(logger != null)
                {
                    logger.LogDebug($"samePath: {samePath} | expected: {_expectedPath} | actual: {context.Request.Path.Value}");
                    logger.LogDebug($"sameMethod: {sameMethod} | expected: {_expectedMethod} | actual: {context.Request.Method}");
                }
            }
#endif
            return _failureObject == null ? "" : JsonConvert.SerializeObject(_failureObject);
        }
    }
}
