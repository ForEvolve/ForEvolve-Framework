using System;
using System.Collections.Generic;
using System.Text;
using ForEvolve.Api.Contracts.Errors;

namespace ForEvolve.Api.Domain.ErrorFactory.Implementations
{
    public class DefaultErrorFromExceptionFactory : IErrorFromExceptionFactory
    {
        public Error Create<TException>(TException exception) where TException : Exception
        {
            throw new NotImplementedException();
        }
    }
}
