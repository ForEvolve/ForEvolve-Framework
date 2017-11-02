using System;
using System.Collections.Generic;
using System.Text;
using ForEvolve.Api.Contracts.Errors;

namespace ForEvolve.Api.Domain.ErrorFactory.Implementations
{
    public class DefaultErrorFromKeyValuePairFactory : IErrorFromKeyValuePairFactory
    {
        private readonly IErrorFromRawValuesFactory _errorFromRawValuesFactory;

        public DefaultErrorFromKeyValuePairFactory(IErrorFromRawValuesFactory errorFromRawValuesFactory)
        {
            _errorFromRawValuesFactory = errorFromRawValuesFactory ?? throw new ArgumentNullException(nameof(errorFromRawValuesFactory));
        }

        public Error Create(string errorCode, KeyValuePair<string, object> errorTargetAndMessage)
        {
            return _errorFromRawValuesFactory.Create(
                errorCode, 
                errorTargetAndMessage.Key, 
                errorTargetAndMessage.Value
            );
        }
    }
}
