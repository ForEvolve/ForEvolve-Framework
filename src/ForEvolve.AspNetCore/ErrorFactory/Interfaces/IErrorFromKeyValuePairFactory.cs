using System.Collections.Generic;
using ForEvolve.Api.Contracts.Errors;

namespace ForEvolve.AspNetCore
{
    public interface IErrorFromKeyValuePairFactory
    {
        Error Create(string errorCode, KeyValuePair<string, object> errorTargetAndMessage);
    }
}
