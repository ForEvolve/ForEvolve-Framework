using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ForEvolve.Contracts.Errors
{
    public class InnerError : Dictionary<string, string>
    {
        [JsonProperty(PropertyName = "code")]
        public string Code
        {
            get { return this["code"]; }
            set { this["code"] = value; }
        }

        [JsonProperty(PropertyName = "hresult")]
        public string HResult
        {
            get { return this["hresult"]; }
            set { this["hresult"] = value; }
        }

        [JsonProperty(PropertyName = "stackTrace")]
        public string StackTrace
        {
            get { return this["stackTrace"]; }
            set { this["stackTrace"] = value; }
        }

        [JsonProperty(PropertyName = "source")]
        public string Source
        {
            get { return this["source"]; }
            set { this["source"] = value; }
        }

        [JsonProperty(PropertyName = "helpLink")]
        public string HelpLink
        {
            get { return this["helpLink"]; }
            set { this["helpLink"] = value; }
        }
    }
}
