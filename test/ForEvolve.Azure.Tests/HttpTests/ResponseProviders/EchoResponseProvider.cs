using System.Reflection;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ForEvolve.XUnit.HttpTests
{
    public class EchoResponseProvider : IResponseProvider
    {
        public string ResponseText(HttpContext context)
        {
            var request = JsonConvert.SerializeObject(
                context.Request,
                new JsonSerializerSettings
                {
                    ContractResolver = new HttpRequestContractResolver()
                }
            );
            return request;
        }

        private class HttpRequestContractResolver : DefaultContractResolver
        {
            public IEnumerable<string> ExcludedProperties { get; }
            public HttpRequestContractResolver()
            {
                ExcludedProperties = new string[]{
                    nameof(HttpRequest.Body),
                    nameof(HttpRequest.HttpContext),
                    nameof(HttpRequest.Form),
                };
            }

            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                return base
                    .CreateProperties(type, memberSerialization)
                    .Where(p => !ExcludedProperties.Contains(p.PropertyName))
                    .ToList();
            }
        }
    }
}
