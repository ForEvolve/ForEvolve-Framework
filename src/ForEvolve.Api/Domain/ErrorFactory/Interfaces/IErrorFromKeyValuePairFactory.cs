using System.Collections.Generic;
using ForEvolve.Api.Contracts.Errors;

namespace ForEvolve.Api.Domain
{
    public interface IErrorFromKeyValuePairFactory
    {
        Error Create(string errorCode, KeyValuePair<string, object> errorTargetAndMessage);
    }
}
