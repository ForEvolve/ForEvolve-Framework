using System.Collections.Generic;
using ForEvolve.Api.Contracts.Errors;

namespace ForEvolve.Api.Domain
{
    public interface IErrorFromDictionaryFactory
    {
        IEnumerable<Error> Create(string errorCode, Dictionary<string, object> details);
    }
}
