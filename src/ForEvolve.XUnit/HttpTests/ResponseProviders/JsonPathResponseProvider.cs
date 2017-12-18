using Microsoft.AspNetCore.Http;
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
            if (context.Request.Path.Value == _expectedPath)
            {
                if (context.Request.Method == _expectedMethod)
                {
                    return JsonConvert.SerializeObject(_successObject);
                }
            }
            return _failureObject == null ? "" : JsonConvert.SerializeObject(_failureObject);
        }
    }
}
