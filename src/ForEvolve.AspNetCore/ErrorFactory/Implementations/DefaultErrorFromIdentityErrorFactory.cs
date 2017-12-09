using System;
using System.Collections.Generic;
using System.Text;
using ForEvolve.Api.Contracts.Errors;
using Microsoft.AspNetCore.Identity;

namespace ForEvolve.AspNetCore.ErrorFactory.Implementations
{
    public class DefaultErrorFromIdentityErrorFactory : IErrorFromIdentityErrorFactory
    {
        private readonly IErrorFromRawValuesFactory _errorFromRawValuesFactory;
        public DefaultErrorFromIdentityErrorFactory(IErrorFromRawValuesFactory errorFromRawValuesFactory)
        {
            _errorFromRawValuesFactory = errorFromRawValuesFactory ?? throw new ArgumentNullException(nameof(errorFromRawValuesFactory));
        }

        public Error Create(IdentityError identityError)
        {
            if (identityError == null) { throw new ArgumentNullException(nameof(identityError)); }

            var error = _errorFromRawValuesFactory.Create(
                identityError.Code,
                nameof(IdentityError),
                identityError.Description
            );
            return error;
        }
    }
}
