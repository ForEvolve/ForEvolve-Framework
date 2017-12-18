using Microsoft.AspNetCore.Http;
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
            if (context.Request.Path.Value == _expectedPath)
            {
                if (context.Request.Method == _expectedMethod)
                {
                    return "";
                }
            }
            throw new NotSupportedException($"Only support: {_expectedMethod} {_expectedPath}");
        }
    }
}
