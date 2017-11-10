using System;
using System.Collections.Generic;
using System.Text;
using ForEvolve.Api.Contracts.Errors;
using System.Linq;

namespace ForEvolve.AspNetCore.ErrorFactory.Implementations
{
    public class DefaultErrorFromDictionaryFactory : IErrorFromDictionaryFactory
    {
        private readonly IErrorFromKeyValuePairFactory _errorFromKeyValuePairFactory;
        public DefaultErrorFromDictionaryFactory(IErrorFromKeyValuePairFactory errorFromKeyValuePairFactory)
        {
            _errorFromKeyValuePairFactory = errorFromKeyValuePairFactory ?? throw new ArgumentNullException(nameof(errorFromKeyValuePairFactory));
        }

        public IEnumerable<Error> Create(string errorCode, Dictionary<string, object> details)
        {
            return details
                .Select(detail => _errorFromKeyValuePairFactory.Create(errorCode, detail));
        }
    }
}
