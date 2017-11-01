using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.Api.Contracts.Errors
{
    public class ErrorResponse
    {
        public ErrorResponse() { }

        public ErrorResponse(Error error)
        {
            Error = error ?? throw new ArgumentNullException(nameof(error));
        }

        [JsonProperty(PropertyName = "error")]
        public Error Error { get; set; }
    }
}
