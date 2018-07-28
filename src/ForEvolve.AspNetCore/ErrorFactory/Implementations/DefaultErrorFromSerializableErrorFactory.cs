using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForEvolve.Contracts.Errors;
using Microsoft.AspNetCore.Mvc;

namespace ForEvolve.AspNetCore.ErrorFactory.Implementations
{
    public class DefaultErrorFromSerializableErrorFactory : IErrorFromSerializableErrorFactory
    {
        private readonly IErrorFromKeyValuePairFactory _errorFromKeyValuePairFactory;

        public DefaultErrorFromSerializableErrorFactory(IErrorFromKeyValuePairFactory errorFromKeyValuePairFactory)
        {
            _errorFromKeyValuePairFactory = errorFromKeyValuePairFactory ?? throw new ArgumentNullException(nameof(errorFromKeyValuePairFactory));
        }

        public Error Create(SerializableError serializableError)
        {
            var error = new Error
            {
                Code = "BadRequest",
                Message = "One or more error occured during model validation.",
                Details = serializableError
                    .Select(v => _errorFromKeyValuePairFactory.Create("ModelStateValidationError", v)).ToList()
            };
            return error;
        }
    }
}
