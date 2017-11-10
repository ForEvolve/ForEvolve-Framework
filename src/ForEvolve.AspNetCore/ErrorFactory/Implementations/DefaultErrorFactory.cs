using System;
using System.Collections.Generic;
using System.Linq;
using ForEvolve.Api.Contracts.Errors;

namespace ForEvolve.AspNetCore.ErrorFactory.Implementations
{
    public class DefaultErrorFactory : IErrorFactory
    {
        private readonly IErrorFromExceptionFactory _errorFromExceptionFactory;
        private readonly IErrorFromDictionaryFactory _errorFromDictionaryFactory;
        private readonly IErrorFromKeyValuePairFactory _errorFromKeyValuePairFactory;
        private readonly IErrorFromRawValuesFactory _errorFromRawValuesFactory;

        public DefaultErrorFactory(
            IErrorFromExceptionFactory errorFromExceptionFactory,
            IErrorFromDictionaryFactory errorFromDictionaryFactory,
            IErrorFromKeyValuePairFactory errorFromKeyValuePairFactory,
            IErrorFromRawValuesFactory errorFromRawValuesFactory
        )
        {
            _errorFromExceptionFactory = errorFromExceptionFactory ?? throw new ArgumentNullException(nameof(errorFromExceptionFactory));
            _errorFromDictionaryFactory = errorFromDictionaryFactory ?? throw new ArgumentNullException(nameof(errorFromDictionaryFactory));
            _errorFromKeyValuePairFactory = errorFromKeyValuePairFactory ?? throw new ArgumentNullException(nameof(errorFromKeyValuePairFactory));
            _errorFromRawValuesFactory = errorFromRawValuesFactory ?? throw new ArgumentNullException(nameof(errorFromRawValuesFactory));
        }

        public Error Create<TException>(TException exception) where TException : Exception
        {
            return _errorFromExceptionFactory.Create(exception);
        }

        public IEnumerable<Error> Create(string errorCode, Dictionary<string, object> details)
        {
            return _errorFromDictionaryFactory.Create(errorCode, details);
        }

        public Error Create(string errorCode, KeyValuePair<string, object> errorTargetAndMessage)
        {
            return _errorFromKeyValuePairFactory.Create(errorCode, errorTargetAndMessage);
        }

        public Error Create(string errorCode, string errorTarget, object errorMessage)
        {
            return _errorFromRawValuesFactory.Create(errorCode, errorTarget, errorMessage);
        }
    }
}
