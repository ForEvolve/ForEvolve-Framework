using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace System
{
    public static class JsonExtensions
    {
        public static HttpContent ToJsonHttpContent(this object model)
        {
            var json = model.ToJson();
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public static string ToJson(this object model)
        {
            var json = JsonConvert.SerializeObject(model);
            return json;
        }

        public static async Task<T> ReadAsJsonObjectAsync<T>(this HttpContent httpContent)
        {
            var json = await httpContent.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json))
            {
                return default(T);
            }
            return json.ToObject<T>();
        }

        public static T ToObject<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
