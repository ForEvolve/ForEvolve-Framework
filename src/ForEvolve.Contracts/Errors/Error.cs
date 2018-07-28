using Newtonsoft.Json;
using System.Collections.Generic;

namespace ForEvolve.Contracts.Errors
{
    public class Error
    {
        public Error(string errorCode = null, string errorMessage = null, string errorTarget = null)
        {
            Code = errorCode;
            Message = errorMessage;
            Target = errorTarget;
        }

        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "target")]
        public string Target { get; set; }

        [JsonProperty(PropertyName = "details")]
        public List<Error> Details { get; set; }

        [JsonProperty(PropertyName = "innerError")]
        public InnerError InnerError { get; set; }
    }
}
