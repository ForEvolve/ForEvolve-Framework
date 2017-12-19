using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace ForEvolve.XUnit.HttpTests
{
    public class EmptyResponseForPathProvider : IResponseProvider
    {
        private readonly string _expectedPath;
        private readonly string _expectedMethod;

        public EmptyResponseForPathProvider(string expectedPath, string expectedMethod)
        {
            _expectedPath = expectedPath ?? throw new ArgumentNullException(nameof(expectedPath));
            _expectedMethod = expectedMethod ?? throw new ArgumentNullException(nameof(expectedMethod));
        }

        public string ResponseText(HttpContext context)
        {
            var samePath = context.Request.Path.Value == _expectedPath;
            var sameMethod = context.Request.Method == _expectedMethod;
            if (samePath && sameMethod)
            {
                return "";
            }
#if DEBUG
            else
            {
                var logger = context.RequestServices?.GetService<ILogger<EmptyResponseForPathProvider>>();
                if (logger != null)
                {
                    logger.LogDebug($"samePath: {samePath} | expected: {_expectedPath} | actual: {context.Request.Path.Value}");
                    logger.LogDebug($"sameMethod: {sameMethod} | expected: {_expectedMethod} | actual: {context.Request.Method}");
                }
            }
#endif
            throw new NotSupportedException($"Only support: {_expectedMethod} {_expectedPath}");
        }
    }
}
