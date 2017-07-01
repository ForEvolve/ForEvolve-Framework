using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ForEvolve.AspNetCore
{
    public class HttpRequestValueFinderException : ArgumentException
    {
        public HttpRequestValueFinderException(string key)
            : base($"No value was found in the current HttpRequest for the specified key: {key}.", nameof(key))
        {

        }
    }
}