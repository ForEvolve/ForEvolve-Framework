using System;
using System.Collections.Generic;
using System.Linq;
using ForEvolve.Contracts.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForEvolve.AspNetCore.ErrorFactory.Implementations
{
    public class DefaultErrorFactory : IErrorFactory
    {
        public static IErrorFactory Current { get; internal set; }

        private readonly IErrorFromExceptionFactory _errorFromExceptionFactory;
        private readonly IErrorFromDictionaryFactory _errorFromDictionaryFactory;
        private readonly IErrorFromKeyValuePairFactory _errorFromKeyValuePairFactory;
        private readonly IErrorFromRawValuesFactory _errorFromRawValuesFactory;
        private readonly IErrorFromIdentityErrorFactory _errorFromIdentityErrorFactory;
        private readonly IErrorFromSerializableErrorFactory _errorFromSerializableErrorFactory;
        private readonly IErrorFromOperationResultFactory _errorFromOperationResultFactory;

        public DefaultErrorFactory(
            IErrorFromExceptionFactory errorFromExceptionFactory,
            IErrorFromDictionaryFactory errorFromDictionaryFactory,
            IErrorFromKeyValuePairFactory errorFromKeyValuePairFactory,
            IErrorFromRawValuesFactory errorFromRawValuesFactory,
            IErrorFromIdentityErrorFactory errorFromIdentityErrorFactory,
            IErrorFromSerializableErrorFactory errorFromSerializableErrorFactory,
            IErrorFromOperationResultFactory errorFromOperationResultFactory
        )
        {
            _errorFromExceptionFactory = errorFromExceptionFactory ?? throw new ArgumentNullException(nameof(errorFromExceptionFactory));
            _errorFromDictionaryFactory = errorFromDictionaryFactory ?? throw new ArgumentNullException(nameof(errorFromDictionaryFactory));
            _errorFromKeyValuePairFactory = errorFromKeyValuePairFactory ?? throw new ArgumentNullException(nameof(errorFromKeyValuePairFactory));
            _errorFromRawValuesFactory = errorFromRawValuesFactory ?? throw new ArgumentNullException(nameof(errorFromRawValuesFactory));
            _errorFromIdentityErrorFactory = errorFromIdentityErrorFactory ?? throw new ArgumentNullException(nameof(errorFromIdentityErrorFactory));
            _errorFromSerializableErrorFactory = errorFromSerializableErrorFactory ?? throw new ArgumentNullException(nameof(errorFromSerializableErrorFactory));
            _errorFromOperationResultFactory = errorFromOperationResultFactory ?? throw new ArgumentNullException(nameof(errorFromOperationResultFactory));
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

        public Error Create(IdentityError identityError)
        {
            return _errorFromIdentityErrorFactory.Create(identityError);
        }

        public Error Create(SerializableError serializableError)
        {
            return _errorFromSerializableErrorFactory.Create(serializableError);
        }

        public Error CreateFrom<TException>(TException exception) where TException : Exception
        {
            return _errorFromExceptionFactory.CreateFrom(exception);
        }

        public Error Create(IOperationResult operationResult)
        {
            return _errorFromOperationResultFactory.Create(operationResult);
        }
    }
}
